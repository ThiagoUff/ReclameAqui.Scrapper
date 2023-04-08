from transformers import AutoTokenizer, DataCollatorWithPadding, BertForPreTraining, TFBertModel
from datasets import load_dataset

import tensorflow as tf
import evaluate
import numpy as np
import pandas as pd

max_seq_length = 128

def main_model():
  encoder = TFBertModel.from_pretrained("neuralmind/bert-base-portuguese-cased")
  input_ids = tf.keras.layers.Input(shape=(max_seq_length,), dtype=tf.int32)
  token_type_ids = tf.keras.layers.Input(shape=(max_seq_length,), dtype=tf.int32)
  attention_mask = tf.keras.layers.Input(shape=(max_seq_length,), dtype=tf.int32)

  embedding = encoder(input_ids, token_type_ids=token_type_ids, attention_mask=attention_mask)[0]

  pooling = tf.keras.layers.GlobalAveragePooling1D()(embedding)
  normalization = tf.keras.layers.BatchNormalization()(pooling)
  dropout = tf.keras.layers.Dropout(0.1)(normalization)

  out = tf.keras.layers.Dense(1, activation="sigmoid", name="final_output_bert")(dropout)

  model = tf.keras.Model(inputs=[input_ids, token_type_ids, attention_mask], outputs=out)

  loss = tf.keras.losses.BinaryCrossentropy(from_logits=True)
  optimizer = tf.keras.optimizers.Adam(lr=2e-5)
  metrics=['accuracy', tf.keras.metrics.FalseNegatives(), tf.keras.metrics.FalsePositives()]

  model.compile(optimizer=optimizer, loss=loss, metrics=metrics)
  return model




dataset = load_dataset("json", data_files="Dataset\\tim.json",  split="train")

dataset = dataset.filter(lambda example: example["VoltariaNegocio"] == False or example["VoltariaNegocio"] == True)
dataset = dataset.rename_column("VoltariaNegocio", "label")
dataset = dataset.rename_column("Descricao", "text")

dataset = dataset.remove_columns(["_id", "Titulo", "Localizacao", "Data", "Categoria", "Produto", "Problema", "Interacoes", "Status", "Resolvido", "Avaliada", "Nota"])

dataset = dataset.train_test_split(test_size=0.1)


tokenizer = AutoTokenizer.from_pretrained('neuralmind/bert-base-portuguese-cased', do_lower_case=False)

def preprocess_function(examples):
    return tokenizer(examples["text"])
    

tokenized_df = dataset.map(preprocess_function, batched = True )


data_collator = DataCollatorWithPadding(tokenizer=tokenizer, return_tensors="tf")

accuracy = evaluate.load("accuracy")
def compute_metrics(eval_pred):
    predictions, labels = eval_pred
    predictions = np.argmax(predictions, axis=1)
    return accuracy.compute(predictions=predictions, references=labels)


id2label = {False: "NEGATIVE", True: "POSITIVE"}
label2id = {"NEGATIVE": False, "POSITIVE": True}

# model = BertForPreTraining.from_pretrained("neuralmind/bert-base-portuguese-cased", num_labels=2, id2label=id2label, label2id=label2id)
# model = main_model()

# batch_size = 16
# num_epochs = 5
# batches_per_epoch = len(tokenized_df["train"]) // batch_size
# total_train_steps = int(batches_per_epoch * num_epochs)
# optimizer, schedule = create_optimizer(init_lr=2e-5, num_warmup_steps=0, num_train_steps=total_train_steps)

# optimizer = tf.keras.optimizers.Adam(learning_rate=5e-5)
tf_train_set = tokenized_df["train"]
tf_validation_set = tokenized_df["test"]

print(tf_train_set)


# tf_validation_set = model.prepare_tf_dataset(
#     tokenized_df["test"],
#     shuffle=False,
#     batch_size=16,
#     collate_fn=data_collator,
# )

# model.compile(optimizer=optimizer)

# metric_callback = KerasMetricCallback(metric_fn=compute_metrics, eval_dataset=tf_validation_set)

# push_to_hub_callback = PushToHubCallback(
#     output_dir="my_awesome_model",
#     tokenizer=tokenizer,
# )

# callbacks = [metric_callback, push_to_hub_callback]

model.fit(x=tf_train_set, validation_data=tf_validation_set, epochs=3)