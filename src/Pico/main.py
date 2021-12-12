from machine import Pin, Timer
from metrics import Metric, Reading
from sensors import cpu_temp, env_temp
import sensors, output

pico_name = "hall"

environment_temperature = env_temp.Ds18x20Temperature(22)

# Initialize metrics and readings
metrics = [
    Metric(
        snake_case_name="cpu_temperature",
        camel_case_name="cpuTemperature",
        min_description="Minimum temperature of the Pico CPU.",
        max_description="Maximum temperature of the Pico CPU.",
        avg_description="Average temperature of the Pico CPU.",
        data_points_description="Number of data points used to calculate the average Pico CPU temperature.",
        reading=Reading(get_value=cpu_temp.read)),
    Metric(
        snake_case_name="environment_temperature",
        camel_case_name="environmentTemperature",
        min_description="Minimum environment temperature.",
        max_description="Maximum environment temperature.",
        avg_description="Average environment temperature.",
        data_points_description="Number of data points used to calculate the average environment temperature.",
        reading=Reading(get_value=environment_temperature.read))
]

def clear_readings():
    for m in metrics:
        m.reading.clear()

def update_readings(timer):
    for m in metrics:
        m.reading.update()

# Start a timer that updates readings
timer = Timer(period=100, mode=Timer.PERIODIC, callback=update_readings)

# Initialize onboare LED
led = Pin(25, Pin.OUT)

# When called, toggle the LED, print and clear readings
def print_readings(output_type: str = "json"):
    led.toggle()
    if output_type == "prometheus":
        output.print_prometheus(pico_name, metrics)
    else:
        output.print_json(pico_name, metrics)
    clear_readings()
