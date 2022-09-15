import { Component, OnInit } from '@angular/core';
import { BooksService } from '../bookservice.component';

@Component({
  selector: 'app-availablebooks',
  templateUrl: './availablebooks.component.html',
  styleUrls: ['./availablebooks.component.css']
})
export class AvailablebooksComponent implements OnInit {
  books:any;

  constructor(public bookServiceComponent : BooksService){
  }

  ngOnInit(): void {
  
      this.bookServiceComponent.getAllBooks()
      .subscribe(
        response => { this.books = response;}
      );
    
  }

}
