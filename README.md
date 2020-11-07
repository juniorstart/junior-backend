# junior-backend

## Business description
The application is to help people applying for a new job position in managing the applications sent. Moreover, it also helps to organize your work through the built-in Todo-list.
The application allows users to integrate themselves through a built-in chat.

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
Maxmium 30 characters

### Login rules
Minimum 4 characters\
Maximum 30 characters\
Must start with character

### Email rules
Not empty\
Must be an email

### FirstName rule
Not empty\
Maximum 30 characters

### LastName rule
Not empty\
Maximum 30 characters