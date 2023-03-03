import panel as pn
import altair as alt
import pandas as pd
import folium as fol
import datetime

from geopy.geocoders import Nominatim

from utils import filter_df

alt.renderers.enable('default')
pn.extension('vega')
alt.data_transformers.enable('csv')


file_name = 'Visualizations\dataset\GeoReclameAqui.xlsx'

geoDf = pd.read_excel(file_name)

geolocator = Nominatim(user_agent="ReclameAqui")

patch = 0

for index, row in geoDf.iterrows():
    if(patch == 200):
        geoDf.to_excel(file_name)
        print("FileSave")
        patch=0
    
    if not(pd.isna(row['location_state'])): continue
   
    try:
        #tries fetch address from geopy
        location = geolocator.geocode(row['Localizacao'] + ", Brasil")
        print(str(index) + " - " + str(location.address) +" - "+ str(datetime.datetime.now()))
       
        #append lat/long to column using dataframe location
        geoDf.loc[index,'location_lat'] = location.latitude
        geoDf.loc[index,'location_long'] = location.longitude
        geoDf.loc[index,'location_state'] = location.raw['display_name'].split(',')[-3]
        geoDf.loc[index,'location_address'] = location.address
    except Exception as e:
        #catches exception for the case where no value is returned
        #appends null value to column
        print(str(index) + " - " + "Error: " + str(e) +" - "+ str(datetime.datetime.now()))
   
        geoDf.loc[index,'location_lat'] = ""
        geoDf.loc[index,'location_long'] = ""
        geoDf.loc[index,'location_address'] = ""
    patch += 1 

# saving the excel
geoDf.to_excel(file_name)