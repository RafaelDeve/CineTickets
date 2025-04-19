import pytest, time, os
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
BASE_URL    = "http://localhost:5219"
PELICULA_ID = 1                               



@pytest.fixture(scope="session")
def driver():
    d = webdriver.Chrome()                   
    d.maximize_window()
    yield d
    d.quit()


def test_pelicula_detalle(driver):
    DETAIL_URL  = f"{BASE_URL}/Peliculas/Detalle/{PELICULA_ID}"
    PROY_URL    = f"{BASE_URL}/Peliculas/Proyecciones?peliculaId={PELICULA_ID}"
    EDIT_URL    = f"{BASE_URL}/Peliculas/Editar?id={PELICULA_ID}"

    
    driver.get(DETAIL_URL)
    time.sleep(1)


    titulo = driver.find_element(By.TAG_NAME, "h1").text.strip()
    assert titulo, "No se encontró el título en la página de detalle."


    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/detalle_before.png")


    driver.find_element(By.XPATH, "//a[contains(@href,'Proyecciones')]").click()
    time.sleep(1)
    assert PROY_URL.lower() in driver.current_url.lower(), "No redirigió a Proyecciones."


    driver.back()
    time.sleep(1)


    driver.find_element(By.XPATH, "//a[contains(@href,'Editar')]").click()
    time.sleep(1)
    assert EDIT_URL.lower() in driver.current_url.lower(), "No redirigió a Editar."


    driver.save_screenshot("screenshots/detalle_after.png")
