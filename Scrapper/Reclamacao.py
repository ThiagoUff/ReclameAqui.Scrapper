class Reclamacao:
    def __init__(self, titulo,
            status,
            reclamacao,
            estado,
            cidade,
            data_criacao,
            id,
            categoria,
            problema,
            produto,
            voltaria_fazer_negocio,
            nota,
            interaction_items,
            url_link):
        self.titulo = titulo
        self.status = status
        self.reclamacao = reclamacao
        self.estado = estado
        self.cidade = cidade
        self.data_criacao = data_criacao
        self.id = id
        self.categoria = categoria
        self.problema = problema
        self.produto = produto
        self.voltaria_fazer_negocio = voltaria_fazer_negocio
        self.nota = nota
        self.interaction_items = None if not interaction_items else interaction_items
        self.url_link = url_link

   