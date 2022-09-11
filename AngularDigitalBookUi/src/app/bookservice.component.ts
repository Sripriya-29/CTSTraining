import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { bookModel } from 'src/books.component';
import { Login } from './login';
import { UserModel } from './users.component';





@Injectable({
  providedIn: 'root'
})
export class BooksService {
  
baseUrl = 'https://localhost:7149/api/Books';
userUrl='https://localhost:7149/api/UserTables';
Authoruserurl ='https://localhost:7145/api/UserTables';
AuthorBookurl ='https://localhost:7145/api/Books';
validateUrl='https://localhost:7077/validate';



constructor(private http: HttpClient) { }



//Get all Books
  getAllBooks():Observable<bookModel[]>{
      return this.http.get<bookModel[]>(this.baseUrl);
  }
  getAllUsers():Observable<UserModel[]>{
    return this.http.get<UserModel[]>(this.userUrl);
}
  //search books by details
  searchBooks(Bname:string,author:string,publisher:string,pd:Date):Observable<bookModel[]>{
    return this.http.get<bookModel[]>(this.baseUrl +'/SearchBook?BName='+Bname+'&Author='+author+'&publisher='+publisher+'&Pd='+pd);
  
  }



 addBook(book: bookModel):Observable<bookModel> {
    return this.http.post<bookModel>(this.AuthorBookurl, book);
  }



 ValidateUsers(userValidation:Login):Observable<Login>
{
  return this.http.post<Login>(this.validateUrl,userValidation);
}
  
postUser(user: UserModel):Observable<UserModel>
{
  console.log('checking');
if(user.roleId=='1')
{
    return this.http.post<UserModel>(this.Authoruserurl,user);
}else
{
  
return this.http.post<UserModel>(this.userUrl,user);
}
  }



 deleteBook(bookId:number):Observable<bookModel>{
    return this.http.delete<bookModel>(this.AuthorBookurl +'/'+bookId);
  }



 updateBook(book: bookModel):Observable<bookModel>{
    return this.http.put<bookModel>(this.baseUrl +'/'+book.bookId,book);
  }

}