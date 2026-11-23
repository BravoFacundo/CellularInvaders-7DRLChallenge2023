# Cellular Invaders — Concepto

**Estado:** Jam con repo (reversionar)
**Género:** Roguelike
**Tamaño estimado:** 2 semanas
**Origen:** Notion (como "Celular Defense"), dic 2022 + 7DRL Challenge 2023

> Nombre único: **Cellular Invaders**. "Celular Defense" fue el nombre provisorio en Notion y ya no se usa.

---

## Concepto
Simulación de una célula con sus sistemas internos (puede ser alienígena e imposible). Hay células en las 4 direcciones y el personaje es un virus que se mueve entre ellas.

- Al conquistar una célula ganás o mejorás habilidades y stats.
- Si un sistema inmune es muy difícil, lo evitás y buscás una célula a la que puedas ganarle.
- Las defensas son cada vez más complejas. Si morís, perdés lo adquirido.
- **Ideal:** un sistema de células procedural (en cantidad y funciones) con funcionamientos planificados. Grilla de células cuadradas, con paredes por donde se puede salir y otras por donde no.

## Estado real
- Se hizo en la **7DRL Challenge 2023** con Daniel Jiménez Morales y equipo, como **Cellular Invaders** (el grupo eligió ese nombre).
- El repo original de Daniel ya no está; Facu armó un repo propio con todas las ramas publicadas.
- **Plan:** la versión de jam queda en la colección "Jams" de itch con créditos al equipo, y la versión mejorada propia sale como otra entrada con el mismo nombre, **Cellular Invaders**, distinguida por versión (por ejemplo, "Jam version" y la versión nueva).

## Estado al pausarlo (nov 2026)

- **Lo que funciona:** la generación de niveles (grilla de células). Ramas publicadas: gameplay-test, dungeon-generation, UI, dungeon-sprites y player.
- **Lo que no:** como juego no es entretenido y el arte no convence. Hace falta rediseñar el loop antes de invertir en pulido.
- **Build web:** el audio usa **Wwise**. Las integraciones de esa época probablemente no exportan a WebGL, así que para una build web habría que reemplazarlo por el audio de Unity.
- **Para mostrarlo sin retomarlo:** la versión de jam va a la colección "Jams" de itch con créditos al equipo, o se muestra con capturas y un clip si Wwise bloquea la build.
