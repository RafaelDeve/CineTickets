
import os
import time
import pytest
from selenium import webdriver
from selenium.webdriver.common.by import By



@pytest.fixture(scope="session")
def driver():
    
    options = webdriver.ChromeOptions()

    driver = webdriver.Chrome(options=options)  
    driver.maximize_window()
    yield driver
    driver.quit()



def test_peliculas_page(driver):
    
    URL = "http://localhost:5219/Peliculas"          
    driver.get(URL)
    time.sleep(1)                                    

    
    os.makedirs("screenshots", exist_ok=True)
    driver.save_screenshot("screenshots/peliculas_before.png")


    body = driver.find_element(By.TAG_NAME, "body").text
    assert "Películas Disponibles" in body, "No se encontró el encabezado esperado."

    cards = driver.find_elements(By.CSS_SELECTOR, "div.card")
    assert cards, "No se encontró ninguna tarjeta de película (.card)."

    driver.save_screenshot("screenshots/peliculas_after.png")
