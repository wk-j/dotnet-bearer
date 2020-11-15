## Bearer

- [x] Swagger UI authorize

```bash
set T (
    echo '{ "user": "xyz", "password": "123" }' \
        | http http://localhost:5000/api/account/getToken \
        | jq -r .accessToken
)

http POST http://localhost:5000/api/hello/hi

http POST http://localhost:5000/api/hello/hi \
    "Authorization: Bearer $T"
```