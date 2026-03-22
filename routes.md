## Users

## Teams
## [GET] /api/team/userId/{userId}
## [GET] /api/team/id/{id}
## [POST] /api/team
### Request
```json
{
  "name": "",
  "description": "",
  "members": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "role": "ADMIN"
    }
  ]
}
```
## [PUT] /api/team
### Request
```json
{
  "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "",
  "description": "",
  "members": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "role": "ADMIN"
    }
  ]
}
```

# Qualifications
## [GET] /api/qualifications/userId/{userId}
## [GET] /api/qualifications/teamId/{teamId}
## [POST] /api/qualifications
### Request
```json
{
  "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "minimumGrade": 1,
  "maximumGrade": 10,
  "passingGrade": 6,
  "period": "Third Period",
  "totalGrades": 5,
  "userQualifications": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "grade": 4,
      "position": 0,
      "hasValue": true,
      "description": ""
    }
  ]
}
```

## Forms

# Auth
## [POST] /api/auth/login
### Request
```json
{
  "email": "test@email.com",
  "password": "123456"
}
```
## [POST] /api/auth/refresh
### Request
```json
{
  "refreshToken": "string"
}
```

# Announcements
## [POST] /api/announcements
### Request
```json
{
  "title": "",
  "content": "",
  "authorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
## [GET] /api/announcements
## [GET] /api/announcements/teamId/{teamId}