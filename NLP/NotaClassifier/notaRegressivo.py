import pandas as pd
import xgboost as xgb
from sklearn.model_selection import train_test_split
from sklearn.metrics import mean_squared_error

# Carregar os dados da base
df = pd.read_excel("Dataset\LiveTimRA.xlsx")


# Separar as features (atributos) e o target (nota)
X = df[['Titulo', 'Localizacao', 'Categoria', 'Problema', 'Produto', 'Descricao']]  # Substitua pelos atributos relevantes da base
y = df['Nota']  # Substitua pela coluna de notas da base

# Dividir os dados em conjunto de treinamento e teste
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# Criar um modelo XGBoost regressor
model = xgb.XGBRegressor()

# Treinar o modelo
model.fit(X_train, y_train)

# Fazer previsões
y_pred = model.predict(X_test)

# Avaliar o modelo
mse = mean_squared_error(y_test, y_pred)
print(f'MSE: {mse}')

# Exemplo de previsão com novos dados
# new_data = pd.DataFrame([[valor1, valor2, valor3]], columns=['Titulo', 'Localizacao', 'Categoria', 'Problema', 'Produto', 'Descricao'])
# prediction = model.predict(new_data)
# print(f'Previsão: {prediction}')
