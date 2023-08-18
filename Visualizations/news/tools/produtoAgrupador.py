servicos_comunicacao = [
    'Internet para casa',
    'Internet para computador',
    'Internet para tablet',
    'Internet para celular',
    'Telefonia fixa',
    'Celular',
    'Internet Banking',
    'Telefone fixo',
    'Smartphone',
    'TV por assinatura',
    'Serviço de entrega',
    'Anúncio',
    'Propaganda enganosa',
    'Atendimento'
]

produtos_eletronicos = [
    'Smartphones',
    'Notebooks',
    'Console (Videogame)',
    'Fone de ouvido',
    'Máquina de Waffle e Creperia',
    'Grill, Sanduicheira e Torradeira',
    'Escovas - Modeladores - Babyliss',
    'Fogão-Forno',
    'TV',
    'Fritadeira elétrica',
    'Console (Videogame)',
    'Secador de cabelo',
    'Depiladores elétricos',
    'Móveis para Jardim e Piscina',
    'Ventiladores e Circuladores de ar',
    'Caixas de som e fone de ouvido',
    'Lavadora de roupas e Tanquinho',
    'Escova de dente',
    'Mouse',
    'Cafeteiras e Chaleiras',
    'Massageadores e Rolos',
    'Aspiradores e vassouras',
    'Computadores all in one',
    'Monitores',
    'Aparelhos de Medição',
    'Impressoras',
    'Filmadora',
    'Máquinas fotográficas-Filmadoras',
    'Vaporizadores e Higienizadores',
    'HDs externos e pen drives',
    'Teclados',
    'Leitores de Ebooks',
    'GPS',
    'Máquinas de cortar o cabelo',
    'Players de DVD-Blue Ray',
    'Máquina fotográfica',
    'Retrovisor',
    'Acessórios e capas',
    'Suportes',
    'Tripés'
]

vestuario_acessorios = [
    'Roupas',
    'Calças',
    'Meias',
    'Blusas',
    'Sapatos',
    'Botas',
    'Sandálias',
    'Bolsas',
    'Bonés e chapéus',
    'Cintos',
    'Chinelos',
    'Camisetas',
    'Saias',
    'Vestidos',
    'Lingerie',
    'Moda Fitness',
    'Camisetas Personalizadas',
    'Uniformes',
    'Fantasias',
    'Maiôs e biquínis',
    'Óculos'
]

produtos_casa = [
    'Móveis',
    'Sofás e Estofados',
    'Tapetes',
    'Móveis para Escritório',
    'Home Theater',
    'Prateleiras e cantoneiras',
    'Armários de cozinha',
    'Cômoda',
    'Escada'
]

alimentos_bebidas = [
    'Chocolates e bombons',
    'Bebidas',
    'Lanches',
    'Café e Cappuccinos',
    'Suco',
    'Água',
    'Fernet',
    'Gin',
    'Whisky',
    'Cerveja',
    'Energéticos'
]

saude_cuidados_pessoais = [
    'Medicamentos e Vitaminas',
    'Proteína De Soja',
    'Emagrecedores',
    'Protetor solar',
    'Tratamento e Anti-idade',
    'Hidratante',
    'Sabonete',
    'Shampoo e Condicionador',
    'Creme',
    'Aparelhos de Medição',
    'Nutricosméticos',
    'Umidificador e Purificador',
    'Mamadeiras',
    'Hidrotônicos'
]

brinquedos_jogos = [
    'Brinquedos eletrônicos',
    'Brinquedos para bebês',
    'Jogos de tabuleiro',
    'Quebra-cabeça',
    'Pelúcias',
    'Bolas'
]

materiais_acessorios_casa = [
    'Caixas organizadoras',
    'Organizadores de cozinha',
    'Organizadores de lavanderia',
    'Organizadores de gaveta',
    'Porta retratos',
    'Prateleiras e cantoneiras',
    'Cabides',
    'Porta-Cartões',
    'Quadros e Cortiças',
    'Estojos e Necessaires',
    'Gaiolas'
]

esportivos = [
    'Bikes',
    'Skates e Pranchas',
    'Esteiras',
    'Steps e trampolim',
    'Tatames e Tapetes',
    'Raquetes e Bolas',
    'Equipamentos de musculação',
    'Bicicletas infantis',
    'Carrinhos e veículos',
    'Mini veículos',
    'Troninhos e assentos',
    'Patinetes'
]

ferramentas_acessorios = [
    'Componentes, peças e acessórios',
    'Adaptadores',
    'Plug de tomada',
    'Conectores',
    'Fechaduras eletrônicas',
    'Disjuntores e armações',
    'Fichários e Acessórios',
    'Cases'
]

def verificar_agrupamento(valor):
    if valor in servicos_comunicacao:
        return 'Serviços de Comunicação'
    elif valor in produtos_eletronicos:
        return 'Produtos Eletrônicos'
    elif valor in vestuario_acessorios:
        return 'Vestuário e Acessórios'
    elif valor in produtos_casa:
        return 'Produtos Casa'
    elif valor in alimentos_bebidas:
        return 'Alimentos e Bebidas'
    elif valor in saude_cuidados_pessoais:
        return 'Saude e Cuidados Pessoais'
    elif valor in brinquedos_jogos:
        return 'Briquendos e Jogos'
    elif valor in materiais_acessorios_casa:
        return 'Materiais e Acessorios Casa'
    elif valor in esportivos:
        return 'Esportivo'
    elif valor in ferramentas_acessorios:
        return 'Ferramentas e Acessorios'
    
    else:
        return 'Outros'
    
def realiza_agrupamento(df):
    df['produto_agrupado'] = df['produto'].apply(verificar_agrupamento)

