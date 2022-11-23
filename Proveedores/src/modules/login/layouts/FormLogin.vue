
<template>

    <form autocomplete="off" id="loginform" onsubmit="return false">
        <section class="contenedor-central">

            <div id="noticias" class="noticias">
            </div>

            <div class="formulario">

                <span>Correo</span>

                <input type="text" required maxlength="50" autocomplete="off" id="txtUsuario" title="Usuario" />

                <span>Contraseña</span>

                <input type="password" required maxlength="20" autocomplete="off" id="txtContrasena"
                    title="Contraseña" />

                <div class="contenedor-controles">
                    <button id="btnIngresar" title="Ingresar a la aplicación ADPROVEEDOR">Ingresar</button>
                    <button id="btnRecordarClave">Olvide mi clave</button>
                    <button id="btnRegistro" title="Registro de un nuevo proveedor para el sistema">Registrarse</button>

                </div>
                <div class="error-login">
                    <p id="msnError">Usuario no valido</p>
                </div>

            </div>

        </section>
    </form>
</template>


<script setup lang="ts">


</script>


<style scoped>


</style>
