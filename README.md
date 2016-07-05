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

## Récupérer le nombre de share d'un record
GET /Records/{id_record}/sharing/count


/////////////////////////
Gestion des records reçus
/////////////////////////

## Récupérer aléatoirement un record de la pool
GET /Player/{id_player}/listenToSpace

## Héberger un record
POST /Record/{id_current_record}/sharedBy/{id_player} { "sharing_id": {id_new_record}}
