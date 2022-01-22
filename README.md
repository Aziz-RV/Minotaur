# P3RV #

Ce dépot contient les sources du projet Unity CONAV (aka P3RV).

### Comment collaborer ? ###

Lors du travail sur une nouvelle fonctionalité :

* S'assurer d'être sur la branche dev-master (`git checkout dev-master`)
* Créer une branche (`git checkout -b <nom>`)
* Faire des commits **réguliers**
* Faire un `git pull` puis un `git push` lorsque l'on est sûrs que les commits sont **fonctionnels** (*merci de ne pas push du code instable*)

Une fois la fonctionalité implémentée, vérifier qu'elle n'a pas cassé le dev-master :

* Merger la branche dev-master (`git merge dev-master`)
* Vérifier en local que tout fonctionne
* Si tout est bon, créer un pull request (cf la [Documentation de Bitbucket](https://confluence.atlassian.com/bitbucket/create-a-pull-request-945541466.html))

### Outils à disposition ###
* [Bugtracker](https://bitbucket.org/ChocOignons2/p3rv/issues), pour gérer les bugs et ouvrir des tâches
* [Wiki](https://bitbucket.org/ChocOignons2/p3rv/wiki/), pour documenter le code