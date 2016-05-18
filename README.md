# The-Coder-Scrolls
## Jeu vidéo pédagogique pour l’apprentissage de la programmation.

Notre projet consiste en la conception d'un jeu vidéo en 3D permettant **l’apprentissage** de la programmation, notamment aux enfants. Le jeu constitue une introduction ludique aux principes de la programmation, à différents niveaux de détail et de technique au gré de l'avancement et de l'intérêt du joueur.

*L’apprentissage se fera à travers le gameplay : le jeu proposera des phases de résolution de problèmes mais sera également modulable en vu de proposer des exercices pratiques.*

## Mécaniques de jeu

*Le gameplay du jeu repose sur l’interaction du joueur avec certains objets de son environnement.*

###Interaction :
Interagir avec un objet ouvre un menu contextuel qui permet au joueur de modifier les propriétés de cet objet ou d’effectuer l’action associée à cet objet.

On interagit avec un objet par 2 moyens (2 touches) :
On peut **utiliser** l’objet (faire une action avec, ex: boire un verre, le remplir, etc…) ou **modifier** son comportement. En survolant l’objet, on peut le modifier en appuyant sur A et l’utiliser en appuyant sur E.
De plus un menu d’informations contenant les variables/objets et leur valeur est disponible.

*Afin de progresser dans le jeu, le joueur devra utiliser certains objets et parfois modifier leurs propriétés avec les bonnes valeurs.
La modification d’une propriété s’effectuera grâce à un champ dans le menu contextuel.*

En cas d’erreur, l’avancement dans le jeu sera bloqué jusqu’à ce que le joueur trouve la solution. Le joueur devra donc **tester** (utiliser) chaque solution afin de savoir s’il s’agit de la bonne.

Lorsque le joueur trouve la solution à une énigme, certains éléments du jeu se modifient afin de laisser le joueur progresser dans l’environnement (par exemple la neige derrière la porte fond et la porte n’est plus bloquée).

#####Menu contextuel 
Il s’agit de la fenêtre principale d’interaction lorsque le joueur souhaite modifier les propriétés d’un objet. 
Cette fenêtre est composée de différents éléments concernant cet objet : 
- un visuel de l’objet (TL)
- une liste des actions possibles sur cet objet (BL)
- des variables à glisser dans les cases du code source au milieu de l’interface (TR)
- une partie où sont affichées les différentes conditions dont dispose le joueur (BR)
- Le code source de cet objet, à modifier en conséquence (C)
