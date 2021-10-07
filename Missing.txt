
- Hacer lo de fadeOut y FadeIn Las funciones de los mixers

- Revisar que otras porpiedades se pueden asignar a las Playlists, pero recordar que las propiedades como volume y eso no se dan porque la idea es que se controled desde un Mixer group
  tal vez alguna funcionalidad

- Funciones del albun para poder poner play automatico a las playlists, osea que se pueda cambiar y automaticamente regrese a la anterior o algo asi
  para casos cuando hay una playlist, pero hay un ataque o evento y se cambia, pero solo x tiempo, luego se regresa

 - Ventanita de monitoreo de audios y eso, ver cuales estan play y todo eso

 - Que al dar en restorevol op pitch se restaure tambien en los sources ya creados y no solo en los file para los que se vana crear nuevos..

 - Issue: No puede haber un fade in vol , porque como tendria que poner play no se sabe si se quiere poner play en objeto o en donde, asi que lo mejor es que
   para lograr este efecto, el programador ponga volume = 0, depues le ponga play y despues Fade(1f), asi se va a hacer el efecto de fade in
   tampoco se puede porque distintas sources tendran diferente volumen asi que se corrompera esto ..

- Pensar como arreglar eso de que los diferentes clips tengan diferente volumen.

- Ver si se puede hacer que la file sea respnsable en una lista de sus clips para si poder hacerles el volumen y el pitch ellos respuesto al maximo de la file.

- Que el size y eso usen int.MinInt para evitar valores negativos