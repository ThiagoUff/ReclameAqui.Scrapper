telecomunicações_serviços = ['Provedores e serv. de internet',
'Telefonia fixa',
'Telefonia celular',
'Internet Banking',
'Serviços de câmbio e transferência de dinheiro',
'Companhias Aéreas',
'Logística e Entrega Rápida',
'TV por assinatura',
'Problemas com o Atendimento',
'Contact Center',
'Exames Lab. e imagem'] 

compra_venda = ['Ecommerce Local',
'Cartões de Crédito',
'Marketplace',
'Meios de pagamentos eletrônicos',
'Problemas na Loja',
'Recuperadora de crédito e cobrança',
'Sites e portais',
'Comparadores de Preço',
'Delivery Alimentação',
'Redes de fast food',
'Supermercados',
'Restaurantes',
'Farmácias',
'Anúncios e Classificados',
'Programas de Fidelidade',
'Agência de Viagens',
'Bomboniere',
'Bebidas',
'Móveis em Geral',
'Eletrodomésticos',
'Cama, Mesa e Banho',
'Tapetes e Carpetes',
'Colchões',
'Iluminação e Elétrica',
'Utilidades domésticas',
'Decoração',
'Papel de Parede e Adesivos',
'Embalagens',
'Livros']

eletronicos_tecnologia = ['Aplicativos',
'Informática',
'Eletroportáteis',
'Eletroeletrônicos',
'Celulares e Smartphones',
'Acessórios para Celulares, Tablets e Computadores',
'Equipamentos de Beleza e Estética',
'Foto e Vídeo',
'Games e Jogos',
'Softwares',
'Assistência técnica',
'Telefones fixos (aparelhos)']

moda_vestuario = ['Acessórios de Vestuário',
'Móveis Modulados',
'Acessórios para Carros',
'Calçados Femininos',
'Bolsas e Malas',
'Calçados Masculinos',
'Moda Infantil',
'Moda Feminina',
'Calçados Infantis',
'Calçados Esportivos',
'Óculos, Lentes e Acessórios',
'Relógios',
'Pilhas e Carregadores',
'Bicicletas']

saude_beleza = ['Higiene e Limpeza Pessoal',
'Corpo e Banho',
'Corpo e Banho Infantil',
'Higiene Bucal',
'Maquiagem',
'Perfumarias',
'Equipamentos de Beleza e Estética',
'Suplementos Alimentares',
'Fitness e Musculação',
'Óculos, Lentes e Acessórios',
'Acessórios para Motos',
'Jóias e Relojoarias',
'Artigos para bebê',
'Acessórios para Bebê',
'Papel de Parede e Adesivos',
'Equipamentos de academia',
'Troca de Fraldas',
'Farmácias']

veiculo_acessorios = ['Autopeças',
'Pneus',
'Chuveiros e Aquecedores',
'Máquinas e equipamentos']

outros = ['Não encontrei meu problema',
'Diversos',
'Milhas']

def verificar_agrupamento(valor):
    if valor in telecomunicações_serviços:
        return 'Telecomunicações e Serviços'
    elif valor in compra_venda:
        return 'Compra e Venda'
    elif valor in eletronicos_tecnologia:
        return 'Eletronicos e Tecnologia'
    elif valor in moda_vestuario:
        return 'Moda e Vestuario'
    elif valor in saude_beleza:
        return 'Saude e Beleza'
    elif valor in veiculo_acessorios:
        return 'Veiculo e Acessórios'
    elif valor in outros:
        return 'Outros'
    
    else:
        return 'Outros'
    
def realiza_agrupamento(df):
    df['categoria_agrupado'] = df['categoria'].apply(verificar_agrupamento)

