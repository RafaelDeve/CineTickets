# test_peliculas_create_fixdate.py
"""
Prueba “Create Película” robusta:

1.  Abre  /Peliculas/Crear
2.  Rellena los campos (fecha vía JavaScript para evitar error de formato)
3.  Screenshot  before  +  after  (y  validation  si falla)
4.  Verifica que el nuevo título está en el listado
"""

import os, time, unicodedata, pytest
from datetime import datetime
from selenium import webdriver
from selenium.webdriver.common.by import By


# ─── CONFIGURA ──────────────────────────────────────────────────────────
BASE_URL = "http://localhost:5219"      # host y puerto de tu app
# ────────────────────────────────────────────────────────────────────────


def normalizar(texto: str) -> str:
    return unicodedata.normalize("NFKD", texto).encode("ascii", "ignore").decode().lower()


@pytest.fixture(scope="session")
def driver():
    drv = webdriver.Chrome()            # Selenium Manager gestiona el driver
    drv.maximize_window()
    yield drv
    drv.quit()


def test_create_pelicula(driver):
    ts      = datetime.now().strftime("%H%M%S")
    today   = datetime.now().strftime("%Y-%m-%d")        # yyyy‑MM‑dd
    TITULO  = f"Película Selenium {ts}"
    GENERO  = "Acción"
    DURACION= "02"                                        # minutos que tu backend acepta

    CREATE_URL = f"{BASE_URL}/Peliculas/Crear"
    LIST_URL   = f"{BASE_URL}/Peliculas"

    # 1) Abrir formulario
    driver.get(CREATE_URL)
    time.sleep(1)

    # 2) Completar campos
    driver.find_element(By.ID, "Titulo").send_keys(TITULO)
    driver.find_element(By.ID, "Descripcion").send_keys("Descripción automática.")
    driver.find_element(By.ID, "Genero").send_keys(GENERO)
    driver.find_element(By.ID, "Duracion").send_keys(DURACION)

    # Fecha vía JS para evitar “value does not conform to format yyyy‑MM‑dd”
    driver.execute_script(
        "document.getElementById('FechaEstreno').value = arguments[0];", today
    )

    # -- Evidencia antes de enviar --
    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/create_before.png")

    # 3) Enviar formulario
    driver.find_element(By.XPATH, "//button[@type='submit']").click()
    time.sleep(1)

    # 4) Si hay errores de validación, capturar y fallar
    errores = driver.find_elements(By.CSS_SELECTOR,
        ".validation-summary-errors li, span.field-validation-error")
    mensajes = [e.text for e in errores if e.text.strip()]
    if mensajes:
        driver.save_screenshot("screenshots/create_validation.png")
        pytest.fail(f"Errores de validación: {mensajes}")

    # 5) Ir al listado y tomar screenshot after
    driver.get(LIST_URL)
    time.sleep(1)
    driver.save_screenshot("screenshots/create_after.png")

    # 6) Verificar que el título aparece
    body_norm   = normalizar(driver.find_element(By.TAG_NAME, "body").text)
    assert normalizar(TITULO) in body_norm, "La película creada no aparece en el listado."
