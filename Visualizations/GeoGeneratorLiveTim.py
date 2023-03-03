import pandas as pd
import datetime
from geopy.geocoders import Nominatim

file_name = 'Visualizations\dataset\LiveTim_maio2022.xlsx'

geoDf = pd.read_excel(file_name)
geoDf['location_lat'] = pd.NA
geoDf['location_long'] = pd.NA
geoDf['location_state'] = pd.NA
geoDf['location_address'] = pd.NA

geolocator = Nominatim(user_agent="LiveTim")

patch = 0

for index, row in geoDf.iterrows():
    if(patch == 200):
        geoDf.to_excel(file_name)
        print("FileSave")
        patch=0
    
    if not(pd.isna(row['location_state'])): continue
   
    try:
        #tries fetch address from geopy
        location = geolocator.geocode(str(row['u_zip_ori']) + "," +str(row['u_city_ori']) +  ", Brasil")
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