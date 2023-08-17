atendimento_relacionamento = ['Mau atendimento no SAC',
'Mau Atendimento',
'Mau atendimento do prestador de serviço',
'Demora no atendimento',
'Funcionários despreparados',
'Fila',
'Descumprimento de acordo',
'Descumprimento de contrato',
'Conta suspensa',
'Cancelamento da conta',
'Forma de pagamento indisponível',
'Não recebi o pagamento']

cobranca_financeiro = ['Cobrança indevida',
'Cobrança duplicada',
'Estorno do valor pago',
'Valor abusivo',
'Descumprimento de prazo',
'Juros abusivos']

qualidade_produto_servico = ['Problema com equipamento fornecido',
'Qualidade da internet',
'Instabilidade do sinal',
'Estabilidade de sinal',
'Qualidade do serviço prestado',
'Baixa Qualidade',
'Produto indisponível',
'Produto errado',
'Produto com defeito',
'Produto não recebido',
'Troca-Devolução de produto',
'Produto usado',
'Produto vencido',
'Tela manchada',
'Quebrado',
'Amassado',
'Peça faltando',
'Peças quebradas',
'Peças erradas',
'Estofamento rasgado',
'Descosturando',
'Machucando',
'Sola soltando',
'Tamanhos diferentes',
'Produto quebrou com pouco tempo de uso',
'Produto estragado',
'Faltando produto na embalagem' ]

compras_pagamento = ['Problemas na finalização da compra',
'Dificuldade de cadastro',
'Pagamento automático',
'Consumo de crédito',
'Consumo da franquia',
'Consumo de Pontos e Milhas',
'Milhas não creditadas',
'Utilização de Pontos e Milhas']

propaganda_publicidade = ['Propaganda enganosa',
'Promoções',
'Promoção',
'Propagada enganosa',
'Promoções',
'Promoção']

acesso_conectividade = ['Não consigo cancelar',
'Não conecta à internet',
'Não liga',
'Não atualiza',
'Lentidão-travamento',
'Não carrega',
'Problemas para utilizar aplicativos',
'Problemas de sinal',
'Problemas com a tela',
'Problemas com as ligações',
'Login-Senha',
'Fila para acessar o site',
'Entrega faltando itens',
'Embalagem danificada',
'Produto danificado',
'Entrega em endereço errado',
'Caixa aberta']

servicos_adicionais = ['Assistência técnica',
'Serasa-SPC',
'Portabilidade',
'Atualização de dados cadastrais',
'SPC-Serasa',
'Pagamento não localizado',
'Resgate de pontos e milhas',
'Pagamento não localizado']

outros = ['Cancelamento',
'Não encontrei meu problema',
'Outro problema',
'Atraso na entrega',
'Segunda via de boleto',
'Site fora do ar-Lento',
'Problema com recarga',
'Problema de fabricação',
'Aba quebrada',
'Problema com o cartão',
'Valor de frete',
'Falta de peças',
'Rasgado',
'Produto com peças faltando',
'Fazendo barulho',
'Descumprimento de prazo',
'Objeto estranho na embalagem',
'Alcance pequeno',
'Quebrada',
'Não gela',
'Dificuldade de cancelamento',
'Roubo-Furto de carga']

def verificar_agrupamento(valor):
    if valor in atendimento_relacionamento:
        return 'Atendimento/Relacionamento'
    elif valor in cobranca_financeiro:
        return 'Cobrança/Financeiro'
    elif valor in qualidade_produto_servico:
        return 'Qualidade do Produto/Serviço'
    elif valor in compras_pagamento:
        return 'Compras Pagamento'
    elif valor in propaganda_publicidade:
        return 'Propaganda e Publicidade'
    elif valor in acesso_conectividade:
        return 'Acesso/Conectividade'
    elif valor in servicos_adicionais:
        return 'Serviços adicionais'
    elif valor in outros:
       return 'Outros'
    else:
        return 'Outros'
    
def realiza_agrupamento(df):
    df['problema_agrupado'] = df['problema'].apply(verificar_agrupamento)

