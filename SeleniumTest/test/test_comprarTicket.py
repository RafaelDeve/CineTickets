
import os, time, pytest
from selenium import webdriver
from selenium.webdriver.common.by import By
BASE_URL      = "http://localhost:5219"
PROYECCION_ID = 1               
USUARIO_ID    = 1             
ASIENTO       = 12              
PRECIO        = 250             



@pytest.fixture(scope="session")
def driver():
    d = webdriver.Chrome()        
    d.maximize_window()
    yield d
    d.quit()


def test_comprar_entrada(driver):
    COMPRA_URL   = f"{BASE_URL}/Entradas/Comprar?proyeccionId={PROYECCION_ID}"
    CONFIRM_URL  = f"{BASE_URL}/Entradas/ConfirmarCompra"

    
    driver.get(COMPRA_URL)
    time.sleep(1)


    assert "Comprar Entrada" in driver.find_element(By.TAG_NAME, "h1").text

    
    driver.find_element(By.ID, "usuarioId").send_keys(str(USUARIO_ID))
    driver.find_element(By.ID, "numeroAsiento").send_keys(str(ASIENTO))
    driver.find_element(By.ID, "precio").send_keys(str(PRECIO))

    
    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/compra_before.png")

    
    driver.find_element(By.CSS_SELECTOR, "button[type='submit']").click()
    time.sleep(1)

    driver.save_screenshot("screenshots/compra_after.png")

    
    assert "/Entradas/ConfirmarCompra" in driver.current_url, "No se redirigió a la confirmación."
