# URL
0.0.0.0:3000/api



# Spin up the server
cd webapp
node .



# Methods

## Créer un Player
POST /Players {"hexid": "hexadecimal_color_code"}


//////////////////////////////
Gestion du records d'un Player
//////////////////////////////

## Créer le record du Player
POST /Players/{id_player}/author {"data": "my records to the universe"}

## Récupérer le record du Player
GET /Players/{id_player}/author

## Détruire le record du Player
DELETE /Records/{id_record}


/////////////////////////
Gestion des records reçus
/////////////////////////

## Récupérer aléatoirement un record de la pool
GET /Player/{id_player}/listenSpace

## Mettre à jour un record
POST /Record/{id_record}/share {"shared": {true | false}}
