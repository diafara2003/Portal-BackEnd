# Comandos para realizar push

sirve para instalar las dependencias del package.json.
npm install

permite dejar de hacerle seguimiento a un archivo ejemplos los debug o release  (ruta: es la ruta del archivo)
git rm --cached  ruta
git rm --cached librerias/Code.Repository.Model/bin/Release/*

ver los archivos modificados o que se les estan haciendo seguimiento
git status

agregar los archivos para seguimiento
git add . 

crear un commit de los cambios (mensaje: es la descripcion del commit)
git commit -am "mensaje"


subir los cambios de la rama al repositorio (rama: es el nombre de la rama)
git push rama

obtener los ultimos cambios de repositorio
git pull


Nota: IMPORTANTE
cada vez que se crea un compilado se debe agregar la ruta al .gitignore esto con el objetivo de que al obtener no se presenten inconsistencias al modificar un rama , si por alguna razon no se tuvo en cuenta se debe quitar el archivo de seguimiento con el siguente comando 

git rm --cached librerias/Code.Repository.Model/bin/Release/*

y luego añadirlo a .gitignore