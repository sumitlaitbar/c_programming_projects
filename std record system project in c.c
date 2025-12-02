
#include <stdio.h>
#include <stdlib.h>
#include <string.h>


struct Student {
    int rollNumber;
    char name[50];
    float marks;
};


void addStudent(struct Student students[], int *count);
void updateStudent(struct Student students[], int count);
void deleteStudent(struct Student students[], int *count);
void displayStudents(struct Student students[], int count);

int main() {
    struct Student students[100]; 
    int count = 0;
    int choice;

    do {
        printf("\nStudent Management System\n");
        printf("1. Add Student\n");
        printf("2. Update Student\n");
        printf("3. Delete Student\n");
        printf("4. Display Students\n");
        printf("5. Exit\n");
        printf("Enter your choice: ");
        scanf("%d", &choice);

        
        switch (choice) {
            case 1:
                addStudent(students, &count);
                break;
            case 2:
                updateStudent(students, count);
                break;
            case 3:
                deleteStudent(students, &count);
                break;
            case 4:
                displayStudents(students, count);
                break;
            case 5:
                printf("Exiting the program. Goodbye!\n");
                break;
            default:
                printf("Invalid choice. Please enter a valid option.\n");
        }

    } while (choice != 5);

    return 0;
}

void addStudent(struct Student students[], int *count) 
{
    
    struct Student newStudent;

    printf("Enter Roll Number: ");
    scanf("%d", &newStudent.rollNumber);
    printf("Enter Name: ");
    scanf("%s", newStudent.name);
    printf("Enter Marks: ");
    scanf("%f", &newStudent.marks);

    students[*count] = newStudent;
    (*count)++;

    printf("Student added successfully!\n");
}

void updateStudent(struct Student students[], int count) 
{
   
    int rollNumber, i;
    int found = 0;

    printf("Enter Roll Number of student to update: ");
    scanf("%d", &rollNumber);

    for (i = 0; i < count; i++) {
        if (students[i].rollNumber == rollNumber) {
            printf("Enter new Name: ");
            scanf("%s", students[i].name);
            printf("Enter new Marks: ");
            scanf("%f", &students[i].marks);
            found = 1;
            printf("Student information updated!\n");
            break;
        }
    }

    if (!found) {
        printf("Student not found with Roll Number %d\n", rollNumber);
    }
}

void deleteStudent(struct Student students[], int *count)
{
   
    int rollNumber, i, found = 0;

    printf("Enter Roll Number of student to delete: ");
    scanf("%d", &rollNumber);

    for (i = 0; i < *count; i++) {
        if (students[i].rollNumber == rollNumber) {
            found = 1;
            
            for (int j = i; j < *count - 1; j++)
            {
                students[j] = students[j + 1];
            }
            (*count)--;
            printf("Student deleted successfully!\n");
            break;
        }
    }

    if (!found) {
        printf("Student not found with Roll Number %d\n", rollNumber);
    }
}

void displayStudents(struct Student students[], int count) 
{
   
    if (count == 0) {
        printf("No students to display.\n");
        return;
    }

    printf("\nList of Students:\n");
    printf("Roll Number\tName\t\tMarks\n");

    for (int i = 0; i < count; i++) {
        printf("%d\t\t%s\t\t%.2f\n", students[i].rollNumber, students[i].name, students[i].marks);
    }
}