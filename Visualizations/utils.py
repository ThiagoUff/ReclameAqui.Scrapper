from typing import List, Union

import pandas as pd

def filter_df(input_data: str) ->  Union[pd.DataFrame, List[str]]:
    
    df = pd.read_excel("dataset.xlsx")
    columns = df.columns
    
    return df, columns