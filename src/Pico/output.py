import json
from metrics import Metric

def print_json(pico: str, metrics: list[Metric]):
    print(json.dumps({
        pico: {
            m.camel_case_name: {
                "min": m.reading.min,
                "max": m.reading.max,
                "avg": m.reading.avg,
                "dataPoints": m.reading.data_points
            } for m in metrics
        }
    }))

def print_prometheus(pico: str, metrics: list[Metric]):
    for m in metrics:
        print(f"""# HELP pico_{m.snake_case_name}_min {m.min_description}
# TYPE pico_{m.snake_case_name}_min gauge
pico_{m.snake_case_name}_min{{pico="{pico}"}} {m.reading.min}
# HELP pico_{m.snake_case_name}_max {m.max_description}
# TYPE pico_{m.snake_case_name}_max gauge
pico_{m.snake_case_name}_max{{pico="{pico}"}} {m.reading.max}
# HELP pico_{m.snake_case_name}_avg {m.avg_description}
# TYPE pico_{m.snake_case_name}_avg gauge
pico_{m.snake_case_name}_avg{{pico="{pico}"}} {m.reading.avg}
# HELP pico_{m.snake_case_name}_data_points {m.data_points_description}
# TYPE pico_{m.snake_case_name}_data_points counter
pico_{m.snake_case_name}_data_points{{pico="{pico}"}} {m.reading.data_points}""")
