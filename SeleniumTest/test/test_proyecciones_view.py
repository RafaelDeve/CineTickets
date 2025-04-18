# test_proyecciones_pelicula.py
"""
Prueba “al estilo simple” para la lista de proyecciones de una película:

1. Abre  /Peliculas/Proyecciones?peliculaId={id}
2. Verifica encabezado  “Proyecciones Disponibles”
3. Confirma que exista al menos una tarjeta .card
4. Hace clic en “Comprar Entradas” de la primera tarjeta
5. Toma capturas antes y después; comprueba que la URL cambie a /Entradas/Comprar
"""

import pytest, time, os
from selenium import webdriver
from selenium.webdriver.common.by import By


# ─── CONFIGURA TU HOST/PUERTO E ID ────────────────────────────
BASE_URL    = "http://localhost:5219"
PELICULA_ID = 1                         # ← id de la película
# ──────────────────────────────────────────────────────────────


@pytest.fixture(scope="session")
def driver():
    d = webdriver.Chrome()              # Selenium Manager descarga driver
    d.maximize_window()
    yield d
    d.quit()


def test_proyecciones_pelicula(driver):
    LIST_URL = f"{BASE_URL}/Peliculas/Proyecciones?peliculaId={PELICULA_ID}"

    # 1) Abrir página de proyecciones de la película
    driver.get(LIST_URL)
    time.sleep(1)

    # 2) Encabezado
    header = driver.find_element(By.TAG_NAME, "h1").text
    assert "Proyecciones Disponibles" in header, "No se encontró el encabezado."

    # 3) Al menos una tarjeta
    cards = driver.find_elements(By.CSS_SELECTOR, "div.card")
    assert cards, "No se encontraron tarjetas de proyección."

    # 4) Captura antes del clic
    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/proy_before.png")

    # 5) Clic en Comprar Entradas de la primera tarjeta
    cards[0].find_element(By.CSS_SELECTOR, "a.btn").click()
    time.sleep(1)

    # 6) Captura después y verificación de redirección
    driver.save_screenshot("screenshots/proy_after.png")
    assert "/Entradas/Comprar" in driver.current_url, "No se redirigió a Entradas/Comprar."
