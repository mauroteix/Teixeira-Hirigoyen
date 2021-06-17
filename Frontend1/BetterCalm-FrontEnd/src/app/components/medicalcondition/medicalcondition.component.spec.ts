import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalconditionComponent } from './medicalcondition.component';

describe('MedicalconditionComponent', () => {
  let component: MedicalconditionComponent;
  let fixture: ComponentFixture<MedicalconditionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicalconditionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicalconditionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
