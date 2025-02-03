-- Insert data into Medicaments table
INSERT INTO Medicaments (Name, Description, Type)
VALUES
    ('Aspirin', 'Pain reliever', 'Tablet'),
    ('Ibuprofen', 'Anti-inflammatory', 'Tablet'),
    ('Paracetamol', 'Pain and fever reliever', 'Tablet'),
    ('Amoxicillin', 'Antibiotic', 'Capsule'),
    ('Metformin', 'Diabetes medication', 'Tablet');

-- Insert data into Patients table
INSERT INTO Patients (FirstName, LastName, Birthdate)
VALUES
    ('John', 'Doe', '1990-05-10'),
    ('Alice', 'Johnson', '1985-07-22'),
    ('Bob', 'Williams', '1992-03-13'),
    ('Eve', 'Taylor', '1988-11-14'),
    ('Michael', 'Brown', '1975-02-27');

-- Insert data into Doctors table
INSERT INTO Doctors (FirstName, LastName, Email)
VALUES
    ('Dr. John', 'Doe', 'john.doe@example.com'),
    ('Dr. Jane', 'Smith', 'jane.smith@example.com'),
    ('Dr. Emily', 'Davis', 'emily.davis@example.com'),
    ('Dr. Michael', 'Johnson', 'michael.johnson@example.com'),
    ('Dr. Sarah', 'Miller', 'sarah.miller@example.com');

-- Insert data into Prescriptions table
-- Assuming Patient IdPatient values are 1, 2, 3, 4, 5 and Doctor IdDoctor values are 1, 2, 3, 4, 5
INSERT INTO Prescriptions (Date, DueDate, IdPatient, IdDoctor)
VALUES
    ('2024-12-29', '2025-01-08', 1, 1),  -- Prescribing for John Doe by Dr. John
    ('2024-12-30', '2025-01-09', 2, 2),  -- Prescribing for Alice Johnson by Dr. Jane
    ('2024-12-30', '2025-01-09', 3, 3),  -- Prescribing for Bob Williams by Dr. Emily
    ('2024-12-31', '2025-01-10', 4, 4),  -- Prescribing for Eve Taylor by Dr. Michael
    ('2024-12-31', '2025-01-10', 5, 5);  -- Prescribing for Michael Brown by Dr. Sarah

-- Insert data into PrescriptionMedicaments table
-- Assuming Medicament IdMedicament values are 1, 2, 3, 4, 5 and Prescription IdPrescription values are 1, 2, 3, 4, 5
INSERT INTO PrescriptionMedicaments (IdMedicament, IdPrescription, Dose, Details)
VALUES
    (1, 1, 2, 'Take twice daily for pain relief'),  -- Aspirin for John Doe
    (2, 2, 1, 'Take once daily for inflammation'),  -- Ibuprofen for Alice Johnson
    (3, 3, 2, 'Take twice daily for pain and fever'),  -- Paracetamol for Bob Williams
    (4, 4, 1, 'Take three times daily for infection'),  -- Amoxicillin for Eve Taylor
    (5, 5, 1, 'Take once daily for diabetes'),  -- Metformin for Michael Brown
    (1, 2, 1, 'Take once daily for pain relief'),  -- Aspirin for Alice Johnson
    (2, 3, 2, 'Take twice daily for inflammation'),  -- Ibuprofen for Bob Williams
    (3, 4, 1, 'Take once daily for fever'),  -- Paracetamol for Eve Taylor
    (4, 5, 1, 'Take once daily for infection');  -- Amoxicillin for Michael Brown
