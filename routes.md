# SageOwl API Routes

## Users
### [GET] /api/users
### [GET] /api/users/id/{id}
### [POST] /api/users
#### Request
```json
{
  "name": "jhon",
  "surname": "doe",
  "email": "test@email.com",
  "password": "123456",
  "username": "myusername",
  "birthday": "2026-03-22"
}
```
### [PUT] /api/users
#### Request
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "jhon",
  "surname": "doe",
  "email": "test@email.com",
  "password": "123456",
  "username": "myusername",
  "birthday": "2026-03-22"
}
```

## Teams
### [GET] /api/team/userId/{userId}
### [GET] /api/team/id/{id}
### [POST] /api/team
#### Request
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
### [PUT] /api/team
#### Request
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

## Qualifications
### [GET] /api/qualifications/userId/{userId}
### [GET] /api/qualifications/teamId/{teamId}
### [POST] /api/qualifications
#### Request
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
### [GET] /api/form/{formId}
### [GET] /api/form/userId/{userId}
### [GET] /api/form/teamId/{teamId}
### [PUT] /api/form
#### Request
```json
{
  "formId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "",
  "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deadline": "2026-03-22",
  "questions": [
    {
      "questionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "title": "",
      "description": "",
      "questionType": "",
      "options": [
        {
          "optionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "value": "",
          "isCorrect": true,
          "isDeleted": false
        }
      ],
      "isDeleted": false
    }
  ]
}
```
### [POST] /api/form
#### Request
```json
{
  "title": "",
  "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deadline": "2026-03-22",
  "questions": [
    {
      "title": "",
      "description": "",
      "questionType": "MULTIPLE_CHOICE",
      "options": [
        {
          "value": "",
          "isCorrect": true
        }
      ]
    }
  ]
}
```

## Auth
### [POST] /api/auth/login
#### Request
```json
{
  "email": "test@email.com",
  "password": "123456"
}
```
### [POST] /api/auth/refresh
#### Request
```json
{
  "refreshToken": "string"
}
```

## Announcements
### [POST] /api/announcements
#### Request
```json
{
  "title": "",
  "content": "",
  "authorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
### [GET] /api/announcements
### [GET] /api/announcements/teamId/{teamId}