# Choix des technologies initiales

## C#
- Langage moderne, orienté objet et typé statiquement.
- Facile à prendre en main, avec une syntaxe claire et un écosystème riche.
- Suffisamment performant pour notre projet tout en offrant une bonne productivité.

## Tour par tour
- Le gameplay au tour par tour rend le jeu plus accessible aux joueurs débutants et facilite la réflexion stratégique.
- Réduit la charge réseau et les accès concurrents à la base de données, simplifiant la logique serveur et la synchronisation des états.

## SQLite
- SQLite est un bon choix pour un jeu tour par tour : installation simple, faible overhead et performances adaptées à des accès peu fréquents.
- Convient pour stocker l’état de la partie, les ressources et les paramètres sans trop de complexité.

## Tour administrateur
- L’administrateur dispose d’un tour dédié pour effectuer la gestion de la partie (ajustement de ressources, modification de paramètres).
- Permet de regrouper et limiter les écritures en bd, et de valider des changements avant leur enregistrement définitif.
- En fin de partie, l’administrateur peut prendre le temps nécessaire pour décider de la clôture ou de la continuation de la partie.