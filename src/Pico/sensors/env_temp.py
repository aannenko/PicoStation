from machine import Pin
from onewire import OneWire
from ds18x20 import DS18X20

class Ds18x20Temperature:
    def __init__(self, gpio: int):
        self._ds = DS18X20(OneWire(Pin(gpio)))
        self._roms = self._ds.scan()

    def read(self) -> float:
        if len(self._roms) < 1:
            return 0.0
        self._ds.convert_temp()
        return self._ds.read_temp(self._roms[0])