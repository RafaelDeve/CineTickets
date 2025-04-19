
import pytest, time, os
from selenium import webdriver
from selenium.webdriver.common.by import By

BASE_URL    = "http://localhost:5219"
PELICULA_ID = 1                         


@pytest.fixture(scope="session")
def driver():
    d = webdriver.Chrome()              
    d.maximize_window()
    yield d
    d.quit()


def test_proyecciones_pelicula(driver):
    LIST_URL = f"{BASE_URL}/Peliculas/Proyecciones?peliculaId={PELICULA_ID}"

    
    driver.get(LIST_URL)
    time.sleep(1)

    
    header = driver.find_element(By.TAG_NAME, "h1").text
    assert "Proyecciones Disponibles" in header, "No se encontró el encabezado."

    
    cards = driver.find_elements(By.CSS_SELECTOR, "div.card")
    assert cards, "No se encontraron tarjetas de proyección."

    
    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/proy_before.png")

    
    cards[0].find_element(By.CSS_SELECTOR, "a.btn").click()
    time.sleep(1)

   
    driver.save_screenshot("screenshots/proy_after.png")
    assert "/Entradas/Comprar" in driver.current_url, "No se redirigió a Entradas/Comprar."
