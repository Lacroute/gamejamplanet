# URL

## Créer un Player
POST /Players {"hexid": "hexadecimal_color_code"}


//////////////////////////////////////////
///// Gestion du message d'un Player /////
//////////////////////////////////////////

## Créer un message pour un Player
POST /Players/{id_player}/author {"data": "my message to the universe"}

## Récupérer le message d'un Player
GET /Players/{id_player}/author

## Détruire le message d'un Player
DELETE /Players/{id_player}/author
