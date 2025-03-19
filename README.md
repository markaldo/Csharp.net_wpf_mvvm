# Phonebook Application (WPF, C#, MVVM)

**A simple phonebook application built using C#, WPF (Windows Presentation Foundation), and the MVVM (Model-View-ViewModel) architectural pattern.**

## Description

This project is a basic phonebook application demonstrating the use of C#, WPF, and the MVVM pattern. It allows users to store and manage a list of contacts, including their names and phone numbers. It's designed as a learning exercise and a practical example of implementing MVVM in a desktop application.

## Features

*   **Add Contacts:**  Allows users to add new contacts to the phonebook.
*   **View Contacts:** Displays a list of all contacts.
*   **Edit Contacts:** Enables users to modify existing contact information.
*   **Delete Contacts:**  Allows users to remove contacts from the phonebook.
*   **MVVM Architecture:** Demonstrates a clean separation of concerns using the Model-View-ViewModel pattern.
*   **Data Binding:** Utilizes data binding to connect the UI (View) to the data (ViewModel).

## Technologies

*   **C#:** The primary programming language.
*   **WPF (Windows Presentation Foundation):** A UI framework for building desktop applications.
*   **MVVM (Model-View-ViewModel):** An architectural pattern that separates the UI, business logic, and data.

## Project Structure

The project is organized according to the MVVM pattern:

*   **Views:** Contains the XAML files that define the user interface (e.g., `MainWindow.xaml`).
*   **ViewModels:** Contains the ViewModel classes that manage the data and logic for the Views (e.g., `MainViewModel.cs`).
*   **Models:** Contains the data models that represent the contact information (e.g., `Contact.cs`).
*   **Stores:** Contains the data and operation on it.
*   **Commands:** Contains the execution logic of the actions or operations.
*   **Components:** Contains the reusable view models.

## Potential Improvements

*   **Data Persistence:** Implement data persistence (e.g., saving contacts to a file or database).
*   **Search Functionality:** Add a search feature to quickly find contacts.
*   **Sorting:** Allow users to sort the contact list.
*   **Input Validation:** Implement input validation to ensure data quality.
*   **More Detailed Contact Information:** Allow users to store additional contact details (e.g., address, email).

## Contributing

Contributions are welcome!  Feel free to submit pull requests or open issues to suggest improvements, report bugs, or add new features.
