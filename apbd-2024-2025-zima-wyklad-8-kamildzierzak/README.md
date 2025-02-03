# Ćwiczenia 8

## Commands

- Add migration - `dotnet ef migrations add 'meaningMigrationName'`

- Undo migration - `dotnet ef migrations remove`

- Apply migration to db - `dotnet ef database update`

## Example Data Showcase for the Database

### 1. Medicaments Table
The `Medicaments` table contains information about different types of medications available in the system.

| **IdMedicament** | **Name**       | **Description**        | **Type**     |
|------------------|----------------|------------------------|--------------|
| 1                | Aspirin        | Pain reliever          | Tablet      |
| 2                | Ibuprofen      | Anti-inflammatory      | Tablet      |
| 3                | Paracetamol    | Pain and fever reliever| Tablet      |
| 4                | Amoxicillin    | Antibiotic             | Capsule     |
| 5                | Metformin      | Diabetes medication    | Tablet      |

---

### 2. Patients Table
The `Patients` table holds information about the patients who visit the doctors.

| **IdPatient** | **FirstName** | **LastName** | **Birthdate**  |
|---------------|---------------|--------------|----------------|
| 1             | John          | Doe          | 1990-05-10     |
| 2             | Alice         | Johnson      | 1985-07-22     |
| 3             | Bob           | Williams     | 1992-03-13     |
| 4             | Eve           | Taylor       | 1988-11-14     |
| 5             | Michael       | Brown        | 1975-02-27     |

---

### 3. Doctors Table
The `Doctors` table contains details of doctors who prescribe medications to patients.

| **IdDoctor** | **FirstName** | **LastName** | **Email**                    |
|--------------|---------------|--------------|------------------------------|
| 1            | Dr. John      | Doe          | john.doe@example.com         |
| 2            | Dr. Jane      | Smith        | jane.smith@example.com       |
| 3            | Dr. Emily     | Davis        | emily.davis@example.com      |
| 4            | Dr. Michael   | Johnson      | michael.johnson@example.com  |
| 5            | Dr. Sarah     | Miller       | sarah.miller@example.com     |

---

### 4. Prescriptions Table
The `Prescriptions` table stores prescriptions given to patients by doctors, including due dates for medication.

| **IdPrescription** | **Date**     | **DueDate**   | **IdPatient** | **IdDoctor** |
|--------------------|--------------|---------------|---------------|--------------|
| 1                  | 2024-12-29   | 2025-01-08    | 1             | 1            |
| 2                  | 2024-12-30   | 2025-01-09    | 2             | 2            |
| 3                  | 2024-12-30   | 2025-01-09    | 3             | 3            |
| 4                  | 2024-12-31   | 2025-01-10    | 4             | 4            |
| 5                  | 2024-12-31   | 2025-01-10    | 5             | 5            |

---

### 5. PrescriptionMedicaments Table
The `PrescriptionMedicaments` table links prescriptions to specific medications and specifies dosages and details for each.

| **IdMedicament** | **IdPrescription** | **Dose** | **Details**                        |
|------------------|--------------------|----------|------------------------------------|
| 1                | 1                  | 2        | Take twice daily for pain relief  |
| 2                | 2                  | 1        | Take once daily for inflammation  |
| 3                | 3                  | 2        | Take twice daily for pain and fever |
| 4                | 4                  | 1        | Take three times daily for infection |
| 5                | 5                  | 1        | Take once daily for diabetes      |
| 1                | 2                  | 1        | Take once daily for pain relief   |
| 2                | 3                  | 2        | Take twice daily for inflammation |
| 3                | 4                  | 1        | Take once daily for fever         |
| 4                | 5                  | 1        | Take once daily for infection     |

---

## Example request body for new prescription:
```
{
  "patient": {
    "idPatient": 1,
    "firstName": "John",
    "lastName": "Doe",
    "birthdate": "1985-03-15T00:00:00Z",
    "prescriptions": [
      {
        "idPrescription": 1,
        "date": "2024-12-29T10:00:00Z",
        "dueDate": "2024-12-31T10:00:00Z",
        "medicaments": [
          {
            "idMedicament": 1,
            "name": "Aspirin",
            "dose": 500,
            "description": "Pain reliever"
          }
        ],
        "doctor": {
          "idDoctor": 1,
          "firstName": "Dr. Sarah",
          "lastName": "Smith",
          "email": "dr.sarah.smith@example.com"
        }
      }
    ]
  },
  "doctor": {
    "idDoctor": 2,
    "firstName": "Dr. James",
    "lastName": "Johnson",
    "email": "dr.james.johnson@example.com"
  },
  "medicaments": [
    {
      "idMedicament": 2,
      "name": "Paracetamol",
      "dose": 500,
      "description": "Pain and fever reducer"
    }
  ],
  "date": "2024-12-29T10:00:00Z",
  "dueDate": "2025-01-05T10:00:00Z"
}

```