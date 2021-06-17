import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookpsychologistComponent } from './bookpsychologist.component';

describe('BookpsychologistComponent', () => {
  let component: BookpsychologistComponent;
  let fixture: ComponentFixture<BookpsychologistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookpsychologistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BookpsychologistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
