from transformers import AutoTokenizer, DataCollatorWithPadding, AutoModelForSequenceClassification , TrainingArguments, Trainer, TFBertModel

from datasets import load_dataset

import evaluate
import numpy as np

dataset = load_dataset("json", data_files="Dataset\\tim.json",  split="train")

dataset = dataset.filter(lambda example: example["VoltariaNegocio"] == False or example["VoltariaNegocio"] == True)
dataset = dataset.rename_column("VoltariaNegocio", "labels")
dataset = dataset.rename_column("Descricao", "text")

dataset = dataset.remove_columns(["_id", "Titulo", "Localizacao", "Data", "Categoria", "Produto", "Problema", "Interacoes", "Status", "Resolvido", "Avaliada", "Nota"])

dataset = dataset.train_test_split(test_size=0.1)


tokenizer = AutoTokenizer.from_pretrained('neuralmind/bert-base-portuguese-cased')

def preprocess_function(examples):
    return tokenizer(examples["text"], truncation=True)
    
print("----- INICIALIZANDO TOKENIZACAO -----")
tokenized_df = dataset.map(preprocess_function, batched = True )
tokenized_df.set_format("torch")
print("----- FINALIZANDO TOKENIZACAO -----")

data_collator = DataCollatorWithPadding(tokenizer=tokenizer)

accuracy = evaluate.load("accuracy")
def compute_metrics(eval_pred):
    predictions, labels = eval_pred
    predictions = np.argmax(predictions, axis=1)
    return accuracy.compute(predictions=predictions, references=labels)


id2label = {False: "NEGATIVE", True: "POSITIVE"}
label2id = {"NEGATIVE": False, "POSITIVE": True}

print("----- INICIALIZANDO MODELO -----")
model = AutoModelForSequenceClassification.from_pretrained("neuralmind/bert-base-portuguese-cased", num_labels=2, id2label=id2label, label2id=label2id)
print("----- FINALIZANDO MODELO -----")

training_args = TrainingArguments(
    output_dir="my_awesome_model",
    learning_rate=2e-5,
    per_device_train_batch_size=16,
    per_device_eval_batch_size=16,
    num_train_epochs=2,
    weight_decay=0.01,
    evaluation_strategy="epoch",
    save_strategy="epoch",
    load_best_model_at_end=True,
    push_to_hub=False,
)

trainer = Trainer(
    model=model,
    args=training_args,
    train_dataset=tokenized_df["train"],
    eval_dataset=tokenized_df["test"],
    tokenizer=tokenizer,
    data_collator=data_collator,
    compute_metrics=compute_metrics,
)

print("----- TREINANDO MODELO -----")
trainer.train()
print("----- FINALIZANDO TREINO MODELO -----")