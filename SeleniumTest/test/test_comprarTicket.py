# test_comprar_entrada.py
"""
Prueba sencilla “Comprar Entrada”

1. Abre  /Entradas/Comprar?proyeccionId={id}
2. Verifica que el H1 contenga “Comprar Entrada”
3. Rellena los campos  usuarioId – numeroAsiento – precio
4. Screenshot   before   y   after
5. Envía y comprueba redirección a /Entradas/ConfirmarCompra
"""

import os, time, pytest
from selenium import webdriver
from selenium.webdriver.common.by import By

# ─── CONFIGURA ───────────────────────────────────────────────
BASE_URL      = "http://localhost:5219"
PROYECCION_ID = 1               # id de la proyección a comprar
USUARIO_ID    = 1              # id de usuario de prueba
ASIENTO       = 12              # número de asiento
PRECIO        = 250             # precio a pagar
# ─────────────────────────────────────────────────────────────


@pytest.fixture(scope="session")
def driver():
    d = webdriver.Chrome()          # Selenium Manager descarga driver correcto
    d.maximize_window()
    yield d
    d.quit()


def test_comprar_entrada(driver):
    COMPRA_URL   = f"{BASE_URL}/Entradas/Comprar?proyeccionId={PROYECCION_ID}"
    CONFIRM_URL  = f"{BASE_URL}/Entradas/ConfirmarCompra"

    # 1) Abrir página de compra
    driver.get(COMPRA_URL)
    time.sleep(1)

    # 2) Verificar encabezado
    assert "Comprar Entrada" in driver.find_element(By.TAG_NAME, "h1").text

    # 3) Rellenar formulario
    driver.find_element(By.ID, "usuarioId").send_keys(str(USUARIO_ID))
    driver.find_element(By.ID, "numeroAsiento").send_keys(str(ASIENTO))
    driver.find_element(By.ID, "precio").send_keys(str(PRECIO))

    # carpeta capturas
    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/compra_before.png")

    # 4) Enviar formulario
    driver.find_element(By.CSS_SELECTOR, "button[type='submit']").click()
    time.sleep(1)

    driver.save_screenshot("screenshots/compra_after.png")

    # 5) Confirmar redirección
    assert "/Entradas/ConfirmarCompra" in driver.current_url, "No se redirigió a la confirmación."
