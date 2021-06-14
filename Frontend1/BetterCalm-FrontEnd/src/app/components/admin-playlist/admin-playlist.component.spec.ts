import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPlaylistComponent } from './admin-playlist.component';

describe('AdminPlaylistComponent', () => {
  let component: AdminPlaylistComponent;
  let fixture: ComponentFixture<AdminPlaylistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminPlaylistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminPlaylistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
