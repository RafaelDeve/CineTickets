import pytest, os, time, unicodedata
from selenium import webdriver
from selenium.webdriver.common.by import By


BASE_URL = "http://localhost:5219"



@pytest.fixture(scope="session")
def driver():
    d = webdriver.Chrome()                 
    d.maximize_window()
    yield d
    d.quit()


def normalizar(txt):
    return unicodedata.normalize("NFKD", txt).encode("ascii", "ignore").decode().lower()


def test_reporte_entradas(driver):
    REPORT_URL = f"{BASE_URL}/Entradas/Reporte"

    
    driver.get(REPORT_URL)
    time.sleep(1)

    
    encabezado = driver.find_element(By.TAG_NAME, "h1").text
    assert "Reporte de Entradas" in encabezado, "No se encontró el encabezado del reporte."

    
    filas = driver.find_elements(By.CSS_SELECTOR, "tbody tr")
    assert filas, "La tabla de entradas está vacía."

    
    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/reporte_before.png")

    
    precios = []
    for fila in filas:
        texto_precio = fila.find_elements(By.TAG_NAME, "td")[3].text.strip()
        valor = float(texto_precio.replace("$", "").replace(",", "."))
        precios.append(valor)
    suma = round(sum(precios), 2)

    
    total_text = driver.find_element(By.TAG_NAME, "h3").text
    total_val = float(
        total_text.split(":")[1]                 
        .replace("$", "")
        .replace(",", ".")
        .strip()
    )
    assert abs(suma - total_val) < 0.01, "El total recaudado no coincide con la suma de la tabla."

    
    driver.find_element(By.LINK_TEXT, "Volver al Inicio").click()
    time.sleep(1)
    assert driver.current_url.rstrip("/") == BASE_URL.rstrip("/"), "No redirigió al inicio."
