FROM postgres:latest

ENV POSTGRES_USER admin1
ENV POSTGRES_PASSWORD admin1234
ENV POSTGRES_DB juniorstart_db

EXPOSE 5432