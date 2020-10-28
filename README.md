# junior-backend

## Run application

``` bash
cd JuniorStart
docker-compose up --build
http://localhost:5001/
```

## Validation rules

### Password regex
``` regex
^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).*
```
Minimum 6 characters\
Maxmium 30 characters\

### Login rules
Minimum 4 characters\
Maximum 30 characters\
Must start with character
