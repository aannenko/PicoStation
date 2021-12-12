class Reading:
    def __init__(self, get_value: function):
        self.min = self.max = self.avg = 0.0
        self.data_points = 0
        self._get_value = get_value

    def update(self):
        value = self._get_value()
        if self.data_points > 0:
            self.min = min(value, self.min)
            self.max = max(value, self.max)
            self.avg += (value - self.avg) / self.data_points
            self.data_points += 1
        else:
            self.min = self.max = self.avg = value
            self.data_points = 1

    def clear(self):
        self.min = self.max = self.avg = 0.0
        self.data_points = 0

class Metric:
    def __init__(self,
                 snake_case_name: str,
                 camel_case_name: str,
                 min_description: str,
                 max_description: str,
                 avg_description: str,
                 data_points_description: str,
                 reading: Reading):
        self.snake_case_name = snake_case_name
        self.camel_case_name = camel_case_name
        self.min_description = min_description
        self.max_description = max_description
        self.avg_description = avg_description
        self.data_points_description = data_points_description
        self.reading = reading
