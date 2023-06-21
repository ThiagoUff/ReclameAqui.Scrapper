import time
import requests
from bs4 import BeautifulSoup
import concurrent.futures

from Reclamacao import Reclamacao
from Repo import MongoDBManager

companies = ['live-tim', 'tim-celular', 'magazine-luiza-loja-online', 'shopee', 'ifood',
             'amazon', 'mercado-livre', 'perfectpay', 'casas-bahia-loja-online', '123-milhas', 'netshoes']


# Definir o cabeçalho de User-Agent
headers = {
    'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36'
}

def soap_find(soap, string, args, is_attr, has_span):
    tag = soap.find(string, attrs=args) if is_attr else soap.find(
        string, class_=args)

    if tag is not None:
        if has_span:
            return tag.span.text.strip()
        return tag.text.strip()
    return None


def parse_details(details_soap, company):
    titulo = soap_find(details_soap, 'h1', {
                       'data-testid': 'complaint-title'}, True, False)
    status = soap_find(details_soap, 'div', {
                       'data-testid': 'complaint-status'},  True, True)
    reclamacao = soap_find(details_soap, 'p', {
                           'data-testid': 'complaint-description'}, True, False)
    localizacao = soap_find(details_soap, 'span', {
                            'data-testid': 'complaint-location'}, True, False)
    data_criacao = soap_find(details_soap, 'span', {
                             'data-testid': 'complaint-creation-date'}, True, False).replace('às ', '')
    categoria = soap_find(details_soap, 'li', {
        'data-testid': 'listitem-categoria'}, True, False)
    problema = soap_find(details_soap, 'li', {
        'data-testid': 'listitem-problema'}, True, False)
    produto = soap_find(details_soap, 'li', {
        'data-testid': 'listitem-produto'}, True, False)
    id = soap_find(details_soap, 'span', {
                   'data-testid': 'complaint-id'}, True, False)[4:]
    voltaria_fazer_negocio = soap_find(
        details_soap, 'p', {'data-testid': 'complaint-deal-again'}, True, False)
    nota = soap_find(details_soap, 'div', "uh4o7z-3 jSJcMd", False, False)

    interactions = details_soap.find_all(
        'dev', attrs={'data-testid': 'complaint-interaction'})
    interaction_items = []
    for interaction in interactions:
        interaction_type = interaction.find('h2')['type']
        interaction_date = interaction.find(
            'span', class_="sc-1o3atjt-3 khRvYe").text.strip()
        interaction_text = interaction.find(
            'p', class_="sc-1o3atjt-4 kBLLZs").text.strip()
        interaction_items.append({'interaction_type': interaction_type,
                                  'interaction_date': interaction_date,
                                  'interaction_text': interaction_text})

    print('Título: ', titulo)
    print('Data: ', data_criacao)
    manager = MongoDBManager(company)

    manager.inserir_reclamacao(Reclamacao(titulo=titulo,
                                          status=status,
                                          reclamacao=reclamacao,
                                          estado=localizacao.split(' - ')[1],
                                          cidade=localizacao.split(' - ')[0],
                                          data_criacao=data_criacao,
                                          id=id,
                                          categoria=categoria,
                                          problema=problema,
                                          produto=produto,
                                          voltaria_fazer_negocio=voltaria_fazer_negocio,
                                          nota=nota,
                                          interaction_items=interaction_items))
    manager.fechar_conexao()


def process_item(item, company):
    start_time = time.time()

    print('--- Reclamação ---')
    detalhe_link = f"https://www.reclameaqui.com.br/{item.find('a')['href']}"

    print(detalhe_link)
    detalhe_response = requests.get(
        detalhe_link, headers=headers)

    if detalhe_response.status_code != 200:
        print(
            f'Erro ao acessar a página {detalhe_link}. Status: {detalhe_response.status_code}')
        return
    detalhe_html = detalhe_response.content
    detalhe_soup = BeautifulSoup(detalhe_html, 'html.parser')

    parse_details(detalhe_soup, company)
    end_time = time.time()

    print('Processamento em:', end_time - start_time)
    print('------------------')
    print()
    print()


def process_company(company):
    base_url = f'https://www.reclameaqui.com.br/empresa/{company}/lista-reclamacoes'
    # Loop para percorrer todas as páginas de reclamações
    pagina = 1
    while True:
        # Montar a URL da página atual
        url = f'{base_url}/?pagina={pagina}'

        # Fazer a requisição HTTP com o cabeçalho de User-Agent
        response = requests.get(url, headers=headers)

        # Verificar se a requisição foi bem-sucedida
        if response.status_code != 200:
            print(
                f'Erro ao acessar a página {url}. Status: {response.status_code}')
            break

        # Criar o objeto BeautifulSoup
        soup = BeautifulSoup(response.text, 'html.parser')

        # Encontrar todas as divs de reclamação
        reclamacoes = soup.find_all('div', class_='sc-1pe7b5t-0 iQGzPh')

        # Verificar se há reclamações na página
        if len(reclamacoes) == 0:
            print(f'Nenhuma reclamação encontrada na página {url}')
            break

        # Loop para processar cada reclamação da página
        with concurrent.futures.ThreadPoolExecutor(max_workers=1) as executor:
            results = executor.map(process_item, reclamacoes, company)
        pagina += 1

    manager = MongoDBManager(company)
    manager.gerar_arquivo_json()
    manager.gerar_arquivo_excel()
    manager.fechar_conexao()


# with concurrent.futures.ThreadPoolExecutor(max_workers=1) as executor:
for company in companies:
    process_company(company)
