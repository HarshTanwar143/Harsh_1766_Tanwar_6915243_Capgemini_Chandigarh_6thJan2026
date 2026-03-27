# Case Study 3 — Production Issue Debugging (Performance Logging)

## What this project does
Tracks execution time of every API call using Log4net.
- Under 3 sec → logs INFO
- Over 3 sec  → logs WARN (slow API detected)
- Exception   → logs ERROR with stack trace

3 log files:
- `logs/app.log`  — all logs
- `logs/slow.log` — only WARN and above (slow APIs)
- `logs/error.log`— only errors

---

## How to Run

```bash
dotnet restore
dotnet run
```

Open Swagger:
```
http://localhost:5000/swagger
```

---

## Test Scenarios

### Fast API — INFO log (< 1 sec)
GET /api/product  → returns all products quickly

### Normal product fetch — INFO log
GET /api/product/1  → fast lookup

### Slow product fetch — WARN log (> 3 sec)
GET /api/product/99  → simulates 4 sec delay

### Slow order fetch — WARN log (always > 3 sec)
GET /api/order  → simulates heavy DB query (3.5 sec)

### Normal order — INFO log
POST /api/order?productId=1&quantity=2

### Crash — ERROR log (always fails)
GET /api/report  → crashes with NullReferenceException after 5 sec

---

## Expected Log Output

```
2026-03-27 10:00:01 INFO  PerformanceMiddleware - Request started — GET /api/product
2026-03-27 10:00:01 INFO  ProductService - API Started — GetAllProducts
2026-03-27 10:00:01 INFO  ProductService - GetAllProducts completed in 0.50 sec
2026-03-27 10:00:01 INFO  PerformanceMiddleware - Request finished — GET /api/product in 0.51 sec

2026-03-27 10:00:10 INFO  PerformanceMiddleware - Request started — GET /api/product/99
2026-03-27 10:00:10 INFO  ProductService - API Started — GetProductById: 99
2026-03-27 10:00:14 WARN  ProductService - Slow API detected — GetProductById took 4.0 sec
2026-03-27 10:00:14 WARN  PerformanceMiddleware - SLOW REQUEST — GET /api/product/99 took 4.0 sec

2026-03-27 10:00:20 INFO  PerformanceMiddleware - Request started — GET /api/report
2026-03-27 10:00:25 ERROR ReportService - API failed — GenerateReport after 5.01 sec
2026-03-27 10:00:25 ERROR ReportController - Report generation failed at controller level
```

---

## Key Concept
The `PerformanceMiddleware` wraps EVERY request with a stopwatch.
This is how real companies detect slow APIs in production without
changing every controller — one middleware catches everything.
