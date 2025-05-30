

import os, time, unicodedata, pytest
from datetime import datetime
from selenium import webdriver
from selenium.webdriver.common.by import By


BASE_URL    = "http://localhost:5219"
PELICULA_ID = 1


def normalizar(t: str) -> str:
    return unicodedata.normalize("NFKD", t).encode("ascii", "ignore").decode().lower()


@pytest.fixture(scope="session")
def driver():
    d = webdriver.Chrome()        
    d.maximize_window()
    yield d
    d.quit()


def test_edit_pelicula(driver):
    ts      = datetime.now().strftime("%H%M%S")
    today   = datetime.now().strftime("%Y-%m-%d")   
    TITULO  = f"Película Editada {ts}"
    GENERO  = "Drama"
    DURACION= "02"

    EDIT_URL = f"{BASE_URL}/Peliculas/Editar?id={PELICULA_ID}"
    LIST_URL = f"{BASE_URL}/Peliculas"

    
    driver.get(EDIT_URL)
    time.sleep(1)

    
    driver.find_element(By.ID, "Titulo").clear()
    driver.find_element(By.ID, "Titulo").send_keys(TITULO)

    driver.find_element(By.ID, "Genero").clear()
    driver.find_element(By.ID, "Genero").send_keys(GENERO)

    driver.find_element(By.ID, "Duracion").clear()
    driver.find_element(By.ID, "Duracion").send_keys(DURACION)

    driver.find_element(By.ID, "Descripcion").clear()
    driver.find_element(By.ID, "Descripcion").send_keys("Descripción actualizada Selenium.")

    
    fecha = driver.find_element(By.ID, "FechaEstreno")
    driver.execute_script("arguments[0].value = arguments[1];", fecha, today)

    
    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/edit_before.png")

    
    driver.find_element(By.XPATH, "//button[@type='submit']").click()
    time.sleep(1)

    
    errores = driver.find_elements(By.CSS_SELECTOR,
        ".validation-summary-errors li, span.field-validation-error")
    textos = [e.text for e in errores if e.text.strip()]
    if textos:
        driver.save_screenshot("screenshots/edit_validation.png")
        pytest.fail(f"Errores de validación: {textos}")

    
    driver.get(LIST_URL)
    time.sleep(1)
    driver.save_screenshot("screenshots/edit_after.png")   

    
    body_norm   = normalizar(driver.find_element(By.TAG_NAME, "body").text)
    assert normalizar(TITULO) in body_norm, "El título editado no aparece en el listado."
