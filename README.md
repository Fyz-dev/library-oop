<h1 align="center">Library</h1>

## Description of the subject area

A library system consists of several basic objects, such as books, authors, and publishers. Books can be of two types: printed (PrintedBook) and electronic (EBook). All books have common properties, but for each type there are additional features.

### Books

The system has a basic **IBook** interface, which contains the general characteristics of the book, such as title, genre, author. Two main types of books are inherited from this class:

- **PrintedBook** are printed books that have physical copies and cover type.
- **EBook** are electronic books that do not have physical copies and are read on electronic devices.

### Authors

**Authors** are an important element of books and can be associated with multiple books. Each author has a name, surname and may have a biography.

### Publishers

**Publishers** are organizations or individuals that publish books. They have a name and an address. A publisher can also be associated with multiple books.

### Library

**Library** (class **Library**) is a container for books. It stores a collection of books and allows you to add, delete, and search for books. The library can work with both types of books — printed and electronic — thanks to polymorphism.

## Class diagram

![diagram](docs/LibraryClassDiagram.png)
