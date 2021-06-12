import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTrackComponent } from './admin-track.component';

describe('AdminTrackComponent', () => {
  let component: AdminTrackComponent;
  let fixture: ComponentFixture<AdminTrackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminTrackComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminTrackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
