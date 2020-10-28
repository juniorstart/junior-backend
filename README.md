# junior-backend

## Run application

``` bash
cd JuniorStart
docker-compose up --build
http://localhost:5001/
```

## Validation rules

# Password regex
``` regex
^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).*
```
Minimum 6 charakters
Maxmium 30 characters

# Login rules
Minimum 4 charakteru
Maximum 30 charakteru
Must start with character
