from pymongo import MongoClient
import json
from openpyxl import Workbook


class MongoDBManager:
    def __init__(self, collection, host='mongodb+srv://usr_master:usr_master@cluster0.ay4a5.mongodb.net/test', port=27017, database='reclame-aqui-py'):
        self.host = host
        self.port = port
        self.database = database
        self.collection = collection
        self.client = MongoClient(host, port)
        self.db = self.client[database]
        self.reclamacoes = self.db[collection]

    def inserir_reclamacao(self, reclamacao):
        reclamacao_data = {
            'titulo': reclamacao.titulo,
            'status': reclamacao.status,
            'reclamacao': reclamacao.reclamacao,
            'estado': reclamacao.estado,
            'cidade': reclamacao.cidade,
            'data_criacao': reclamacao.data_criacao,
            'id': reclamacao.id,
            'categoria': reclamacao.categoria,
            'problema': reclamacao.problema,
            'produto': reclamacao.produto,
            'voltaria_fazer_negocio': reclamacao.voltaria_fazer_negocio,
            'nota': reclamacao.nota,
            'interaction_items': reclamacao.interaction_items,
            'url_link': reclamacao.url_link
        }
        query = {'id': reclamacao.id}
        result = self.reclamacoes.update_one(
            query, {'$set': reclamacao_data}, upsert=True)

        if result.upserted_id:
            print("Reclamação inserida no banco de dados. ID:", result.upserted_id)
        else:
            print("Reclamação atualizada no banco de dados. ID:", reclamacao.id)

    def get_all(self):
        elements = list(self.reclamacoes.find({}))
        for element in elements:
            element['empresa'] = self.collection
        
        return elements
    
    def fechar_conexao(self):
        self.client.close()
        print("Conexão com o banco de dados fechada.")