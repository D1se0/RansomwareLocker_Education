# 🛡️ RansomwareSim

RansomwareSim es una herramienta educativa que simula el funcionamiento básico de un ransomware. Está diseñada para fines didácticos y de concienciación, permitiendo entender cómo operan estos programas maliciosos en un entorno controlado.

> ⚠️ Advertencia: Este proyecto es únicamente para uso educativo y pruebas en entornos seguros. No utilices este software en sistemas productivos o con datos reales. El autor no se hace responsable del mal uso.

## 📦 Características

🔒 Cifra archivos en el directorio de usuario (`C:\Users\<Usuario>`).

🔓 Descifra archivos usando la clave secreta.

📝 Genera una nota de rescate en el escritorio (`README_RESTORE_FILES.txt`).

🐞 Modo debug para ver información detallada durante la ejecución.

🛑 Excluye archivos críticos del sistema para evitar daños.

## 🚀 Instalación

1. Clona el repositorio

```bash
git clone https://github.com/D1se0/RansomwareLocker_Education.git
cd RansomwareLocker_Education
```

2. Compila el proyecto

  - Abre el proyecto en Visual Studio y compílalo (target `.NET 6` o superior).

  - O compila desde la terminal:

  ```bash
  dotnet build -c Release
  ```

3. Ejecuta el programa

  ```bash
  dotnet run --project RansomwareSim
  ```

  - Tambien se puede ejecutar con el `.exe` que proporciona cuando se compila

## 🛠️ Uso

Cuando inicies la aplicación verás un menú como este:

```bash
=== 🛡 Ransomware Educativo ===
1. Cifrar archivos
2. Descifrar archivos
Elige una opción (1/2) o escribe 'debug' para activar modo debug:
```

### 🔒 Cifrar archivos

1. Selecciona la opción 1.

2. Todos los archivos en tu carpeta de usuario serán cifrados y se añadirá la extensión `.locked`.

> NOTA: Si al aplicar el icono diera algun error, simplemente tendreis que adaptar vuestra ruta de donde este el `.ico` en el archivo `set_locked_icon.reg` en esta linea `C:\\Users\\<USER>\\Desktop\\RansomwareLocker_Education\\locked.ico`

3. Se generará una nota de rescate en el escritorio:

```less
💀 Tus archivos han sido cifrados 💀

Todos tus documentos, fotos y videos están encriptados.
Para recuperarlos necesitas la clave secreta.

Envía 1 BTC a la siguiente dirección:
👉 1FfmbHfnpaZjKFvyi1okTjJJusN455paPH 👈

Después, envía un correo a: hacker@falsoemail.com

⚠️ Si no pagas en 24h perderás tus archivos para siempre.
```

### 🔓 Descifrar archivos

1. Selecciona la opción 2.

2. Introduce la clave secreta configurada en el código (`uYbVcXeZgUkXp2s5v8y/B?E(H+MbQeTh`).

3. Si la clave es correcta, los archivos serán restaurados a su estado original.

### 🐞 Modo Debug

Para activar el modo debug, escribe `debug` en el menú principal. Esto mostrará detalles como:

- Carpetas ignoradas.

- Archivos cifrados/descifrados con éxito.

- Errores de acceso o permisos.

## ⚠️ Advertencia Legal

Este proyecto es una simulación educativa. No está destinado a ser utilizado como malware real ni en entornos productivos. Su propósito es enseñar y concienciar sobre los riesgos de los ransomware.

✅ Úsalo solo en máquinas virtuales o entornos aislados.
