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
        }
        query = {'id': reclamacao.id}
        result = self.reclamacoes.update_one(
            query, {'$set': reclamacao_data}, upsert=True)

        if result.upserted_id:
            print("Reclamação inserida no banco de dados. ID:", result.upserted_id)
        else:
            print("Reclamação atualizada no banco de dados. ID:", reclamacao.id)

    def gerar_arquivo_json(self):
        documents = self.collection.find()
        json_documents = []
        for document in documents:
            json_document = json.dumps(document, default=str)
            json_documents.append(json_document)
        with open(f'{self.collection}.json', 'w') as arquivo:
            json.dump(json_documents, arquivo)



    def gerar_arquivo_excel(self):
        documents = self.collection.find()
        # Criação do objeto Workbook
        wb = Workbook()

        # Seleciona a planilha ativa
        ws = wb.active
        ws['A1'] = 'id'
        ws['B1'] = 'titulo'
        ws['C1'] = 'status'
        ws['D1'] = 'data_criacao'
        ws['E1'] = 'reclamacao'
        ws['F1'] = 'estado'
        ws['G1'] = 'cidade'
        ws['H1'] = 'categoria'
        ws['I1'] = 'problema'
        ws['J1'] = 'produto'
        ws['L1'] = 'voltaria_fazer_negocio'
        ws['M1'] = 'nota'

        for index in range(0, len(documents)):
            ws[f'A{index+2}'] = documents[index]['id']
            ws[f'B{index+2}'] = documents[index]['titulo']
            ws[f'C{index+2}'] = documents[index]['status']
            ws[f'D{index+2}'] = documents[index]['data_criacao']
            ws[f'E{index+2}'] = documents[index]['reclamacao']
            ws[f'F{index+2}'] = documents[index]['estado']
            ws[f'G{index+2}'] = documents[index]['cidade']
            ws[f'H{index+2}'] = documents[index]['categoria']
            ws[f'I{index+2}'] = documents[index]['problema']
            ws[f'J{index+2}'] = documents[index]['produto']
            ws[f'L{index+2}'] = documents[index]['voltaria_fazer_negocio']
            ws[f'M{index+2}'] = documents[index]['nota']
            
        wb.save('exemplo.xlsx')
        
    
    def fechar_conexao(self):
        self.client.close()
        print("Conexão com o banco de dados fechada.")