from machine import ADC

_adc = ADC(4)
_voltage_factor = 3.3 / 65535

def read() -> float:
    return 27 - ((_adc.read_u16() * _voltage_factor) - 0.706) / 0.001721
