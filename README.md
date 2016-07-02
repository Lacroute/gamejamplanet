# URL
0.0.0.0:3000/api



# Spin up the server
cd webapp
node .



# Methods

## Créer un Player
POST /Players {"hexid": "hexadecimal_color_code"}
POST /Players{id_player}/handlerecord


//////////////////////////////
Gestion du records d'un Player
//////////////////////////////

## Créer le record d'un Player
POST /Players/{id_player}/author {"data": "my records to the universe"}

## Récupérer le records d'un Player
GET /Players/{id_player}/author

## Détruire le records d'un Player
DELETE /Players/{id_player}/author


/////////////////////////
Gestion des records reçus
/////////////////////////

## Récupérer les pendings d'un Player
GET /Pendingboxes/{id_player}/target

## Modifier la target d'un Record
PUT /Pendingboxes/{id_player}/target/{id_record} {"status": "sent", "target_id": {new_id}}
