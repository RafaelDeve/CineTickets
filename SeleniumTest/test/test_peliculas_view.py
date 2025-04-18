# test_peliculas_view.py
import os
import time
import pytest
from selenium import webdriver
from selenium.webdriver.common.by import By


# ---------- FIXTURE: WebDriver administrado por Selenium Manager ----------
@pytest.fixture(scope="session")
def driver():
    """
    • No se especifica Service ni executable_path.
    • Selenium Manager descarga el driver adecuado la primera vez.
    """
    options = webdriver.ChromeOptions()
    # Si prefieres modo headless, descomenta la línea siguiente:
    # options.add_argument("--headless=new")

    driver = webdriver.Chrome(options=options)  #  ←  Selenium Manager se encarga
    driver.maximize_window()
    yield driver
    driver.quit()


# ---------- TEST: Página de listado de películas ----------
def test_peliculas_page(driver):
    """
    • Abre /Peliculas
    • Verifica el encabezado y que exista al menos una tarjeta .card
    • Guarda screenshots: antes y después
    """
    URL = "http://localhost:5219/Peliculas"          # Ajusta puerto/ruta si cambia
    driver.get(URL)
    time.sleep(1)                                    # espera simple (puedes usar WebDriverWait)

    # Carpeta para evidencias
    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/peliculas_before.png")

    # Verificaciones
    body = driver.find_element(By.TAG_NAME, "body").text
    assert "Películas Disponibles" in body, "No se encontró el encabezado esperado."

    cards = driver.find_elements(By.CSS_SELECTOR, "div.card")
    assert cards, "No se encontró ninguna tarjeta de película (.card)."

    driver.save_screenshot("screenshots/peliculas_after.png")
