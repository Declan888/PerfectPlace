﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PerfectPlace.Models;

using PagedList;
using PerfectPlace.ViewModels;

namespace PerfectPlace.Controllers
{
    public class SuburbController : Controller
    {
        private SuburbEntities db = new SuburbEntities();
        //private PreferenceEntities suburb = new PreferenceEntities();
        private SuburbDataEntities suburbData = new SuburbDataEntities();

        // This page will deal with the algorithm of searching function
        // GET: /Suburb/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.Current = "Suburb";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var suburbs = from s in db.suburb_info select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                suburbs = suburbs.Where(s => s.community_name.Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    suburbs = suburbs.OrderByDescending(s => s.community_name);
                    break;
                default:
                    suburbs = suburbs.OrderBy(s => s.community_name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            //return View(db.suburb_info.ToList());
            return View(suburbs.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Suburb/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    suburb_info suburb_info = db.suburb_info.Find(id);
        //    if (suburb_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(suburb_info);
        //}

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    rating_it2 suburb_info = suburb.rating_it2.Find(id);
        //    //suburb_info suburb_info = db.suburb_info.Find(id);
        //    if (suburb_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(suburb_info);
        //}

        public ActionResult Details(int? id, int lay = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rating_it3 suburbInfo = suburbData.rating_it3.Find(id);
            //myModel.SuburbInfo = suburbInfo;
            //myModel.SuburbListData = suburbList;
            //myModel.SuburbListData = suburbList;
            //suburb_info suburb_info = db.suburb_info.Find(id);

            if (suburbInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.lay = lay;
            return View(suburbInfo);
        }

        // GET: /Suburb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Suburb/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="community_name,region,map_reference,grid_reference,location,population_density,travel_time_gpo,distance_to_gpo,lga,primary_care_partnership,medicare_local,area,aria_min,aria_max,aria_avg,abs_remoteness_category,dhs_area,commercial,commercial_percent,industrial,industrial_percent,residential,residential_percent,rural,rural_percent,other,other_percent,erp_age_0_4_2012,erp_age_0_4_percent_2012,erp_age_5_9_2012,erp_age_5_9_percent_2012,erp_age_10_14_2012,erp_age_10_14_percent_2012,erp_age_15_19_2012,erp_age_15_19_percent_2012,erp_age_20_24_2012,erp_age_20_24_percent_2012,erp_age_25_44_2012,erp_age_25_44_percent_2012,erp_age_45_64_2012,erp_age_45_64_percent_2012,erp_age_65_69_2012,erp_age_65_69_percent_2012,erp_age_70_74_2012,erp_age_70_74_percent_2012,erp_age_75_79_2012,erp_age_75_79_percent_2012,erp_age_80_84_2012,erp_age_80_84_percent_2012,erp_age_85_plus_2012,erp_age_85_plus_percent_2012,erp_total_2012,erp_age_0_4_2007,erp_age_0_4_percent_2007,erp_age_5_9_2007,erp_age_5_9_percent_2007,erp_age_10_14_2007,erp_age_10_14_percent_2007,erp_age_15_19_2007,erp_age_15_19_percent_2007,erp_age_20_24_2007,erp_age_20_24_percent_2007,erp_age_25_44_2007,erp_age_25_44_percent_2007,erp_age_45_64_2007,erp_age_45_64_percent_2007,erp_age_65_69_2007,erp_age_65_69_percent_2007,erp_age_70_74_2007,erp_age_70_74_percent_2007,erp_age_75_79_2007,erp_age_75_79_percent_2007,erp_age_80_84_2007,erp_age_80_84_percent_2007,erp_age_85_plus_2007,erp_age_85_plus_percent_2007,erp_total_2007,change_2007_2012_age_0_4,change_2007_2012_age_5_9,change_2007_2012_age_10_14,change_2007_2012_age_15_19,change_2007_2012_age_20_24,change_2007_2012_age_25_44,change_2007_2012_age_45_64,change_2007_2012_age_65_69,change_2007_2012_age_70_74,change_2007_2012_age_75_79,change_2007_2012_age_80_84,change_2007_2012_age_85_plus,change_2007_2012_total,public_hospitals,private_hospitals,community_health_centres,bush_nursing_centres,allied_health,alternative_health,child_protection_and_family,dental,disability,general_practice,homelessness,mental_health,pharmacies,aged_care_high_care,aged_care_low_care,aged_care_srs,childcare,primary_schools,secondary_schools,p12_schools,other_schools,centrelink_offices,medicare_offices,medicare_access_points,number_of_households,average_persons_per_household,occupied_private_dwellings,occupied_private_dwellings_percent,population_non_private_dwellings,public_housing_dwellings,public_housing_dwellings_percent,dwellings_no_motor_vehicle,dwellings_no_motor_vehicle_percent,dwellings_with_no_internet,dwellings_with_no_internet_percent,equivalent_household_income_lessthan_600,equivalent_household_income_lessthan_600_percent,personal_income_lessthan_400,personal_income_lessthan_400_percent,number_of_families,female_headed_lone_parent_families,female_headed_lone_parent_families_percent,male_headed_lone_parent_families,male_headed_lone_parent_families_percent,residing_near_pt_percent,irsd_min,irsd_max,irsd_avg,primary_school_students,secondary_school_students,tafe_students,university_students,holds_degree_persons,holds_degree_persons_percent,did_not_complete_year_12,did_not_complete_year_12_percent,unemployed_persons,unemployed_persons_percent,volunteers_persons,volunteers_persons_percent,requires_assistance_core_activities_persons,requires_assistance_core_activities_persons_percent,aged_75_lives_alone_persons,aged_75_lives_alone_persons_percent,unpaid_carer_to_disability,unpaid_carer_to_disability_percent,unpaid_carer_children,unpaid_carer_children_percent,top_industry_1,top_industry_percent_1,top_industry_persons_2,top_industry_percent_2,top_industry_persons_3,top_industry_percent_3,top_occupation_1,top_occupation_percent_1,top_occupation_2,top_occupation_percent_2,top_occupation_3,top_occupation_percent_3,aboriginal_torres_strait_islander,aboriginal_torres_strait_islander_percent,born_overseas,born_overseas_percent,born_non_english_speaking_country,born_non_english_speaking_country_percent,speaks_lote_home,speaks_lote_home_percent,poor_english_proficiency,poor_english_proficiency_percent,top_country_of_birth_1,top_country_of_birth_persons_1,top_country_of_birth_percent_1,top_country_of_birth_2,top_country_of_birth_persons_2,top_country_of_birth_percent_2,top_country_of_birth_3,top_country_of_birth_person_3,top_country_of_birth_percent_3,top_country_of_birth_4,top_country_of_birth_person_4,top_country_of_birth_percent_4,top_country_of_birth_5,top_country_of_birth_person_5,top_country_of_birth_percent_5,top_language_spoken_1,top_language_spoken_person_1,top_language_spoken_percent_1,top_language_spoken_2,top_language_spoken_persons_2,top_language_spoken_percent_2,top_language_spoken_3,top_language_spoken_persons_3,top_language_spoken_percent_3,top_language_spoken_4,top_language_spoken_persons_4,top_language_spoken_pecent_4,top_language_spoken_5,top_language_spoken_persons_5,top_language_spoken_percent_5,public_hospital_separations,nearest_public_hospital,travel_time_nearest_public_hospital,distance_nearest_public_hospital,obstetric_type_separations,public_hospital_maternity_services,public_hospital_maternity_services_time,public_hospital_maternity_services_distance,presentations_emergency_departments,public_hospital_emergency_department,public_hospital_emergency_department_time,public_hospital_emergency_department_distance,presentations_emergency_departments_injury,presentations_emergency_departments_injury_percent,emergency_department_presentations,emergency_department_presentations_percent,suburb_id,postcode,suburb,state,dc,type,lat,lon")] suburb_info suburb_info)
        {
            if (ModelState.IsValid)
            {
                db.suburb_info.Add(suburb_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(suburb_info);
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Test1(rating_it2 modelData)
        {
            return View(modelData);
        }

        public ActionResult Test2(rating_it3 modelData)
        {
            SuburbAndSuburbList model = new SuburbAndSuburbList();

            object suburbSearch = null;
            string nationality = "";
            string suburbChange = "";
            string inputSuburb = "";

            if (Session["searchMethod"] == "Index")
            {
                suburbSearch = Session["suburbSearch"] == null ? null : Session["suburbSearch"];
                nationality = Session["nationality"] == null ? null : Session["nationality"].ToString();
                model.SuburbListData = Search_Index_((suburbSearch as int[]), nationality);
            }
            else if (Session["searchMethod"] == "SearchBySuburb")
            {
                inputSuburb = Session["inputSuburb"] == null ? null : Session["inputSuburb"].ToString();
                model.SuburbListData = Search_SearchBySuburb_(inputSuburb);
            }
            else if (Session["searchMethod"] == "SearchBySuburbChange")
            {
                ViewBag.SearchResultPage = "SearchByLifeStyle";
                suburbChange = Session["suburbChange"] == null ? null : Session["suburbChange"].ToString();
                if (suburbChange != null && suburbChange.Equals("I want to live near by the sea"))
                {
                    ViewBag.LifeStyle = "SeaChange";
                }
                else if (suburbChange != null && suburbChange.Equals("I want to live near by the green"))
                {
                    ViewBag.LifeStyle = "TreeChange";
                }
                else if (suburbChange != null && suburbChange.Equals("I want to live near by the city"))
                {
                    ViewBag.LifeStyle = "CityChange";
                }
                model.SuburbListData = Search_SearchBySuburbChange_(suburbChange);
            }

            model.SuburbData = modelData;

            return View(model);
        }



        #region these are same functions from search controller ,if criteria changes there it should also change
        public List<rating_it3> Search_Index_(int[] suburbSearch, string nationality)//this is same method from search controller
        {
            List<rating_it3> listData = new List<rating_it3>();
            rating_it3 data = new rating_it3();
            SuburbDataEntities db1 = new SuburbDataEntities();



            string veryNearDistanceToCity = null;
            string veryHighMoreshops = null;
            string veryHighHealthServices = null;
            string lowAccidentRate = null;
            string veryHighMoreAgedcare = null;
            string veryLessTimeToHospital = null;
            string lowCrimeRate = null;
            string topCountryOfBirth = null;

            #region codnition1
            if (suburbSearch == null && nationality.Equals("Preferred Nationality"))
            {
                ViewBag.veryNearDistanceToCity = null;
                ViewBag.veryHighMoreshops = null;
                ViewBag.veryHighHealthServices = null;
                ViewBag.lowAccidentRate = null;
                ViewBag.veryHighMoreAgedcare = null;
                ViewBag.veryLessTimeToHospital = null;
                ViewBag.lowCrimeRate = null;
                ViewBag.topCountryOfBirth = null;
                ViewBag.resultCount = 0;
                return null;
            }
            #endregion


            #region condition2
            if (suburbSearch == null && !nationality.Equals("Preferred Nationality"))
            {
                ViewBag.veryNearDistanceToCity = null;
                ViewBag.veryHighMoreshops = null;
                ViewBag.veryHighHealthServices = null;
                ViewBag.lowAccidentRate = null;
                ViewBag.veryHighMoreAgedcare = null;
                ViewBag.veryLessTimeToHospital = null;
                ViewBag.lowCrimeRate = null;
                ViewBag.topCountryOfBirth = nationality;
                ViewBag.resultCount = db1.SearchByPreference(veryNearDistanceToCity, veryHighMoreshops, veryHighHealthServices, lowAccidentRate, veryHighMoreAgedcare, veryLessTimeToHospital, lowCrimeRate, topCountryOfBirth, null).Count();


                IEnumerable<rating_it3> reslt1 = db1.SearchByPreference(veryNearDistanceToCity, veryHighMoreshops, veryHighHealthServices, lowAccidentRate, veryHighMoreAgedcare, veryLessTimeToHospital, lowCrimeRate, topCountryOfBirth, null);

                foreach (var item in reslt1)
                {
                    data.suburb_id = item.suburb_id;
                    data.suburb = item.suburb;
                    data.distance_to_city_rate = item.distance_to_city_rate;
                    data.population_density = item.population_density;

                    data.distance_to_city = item.distance_to_city;
                    data.commercial_percent = item.commercial_percent;
                    data.commercial_rate = item.commercial_rate;
                    data.health_services = item.health_services;
                    data.accident_count = item.accident_count;
                    data.accident_count_rate = item.accident_count_rate;
                    data.aged_care = item.aged_care;
                    data.time_to_hospital = item.time_to_hospital;
                    data.offence_count = item.offence_count;
                    data.crime_rate = item.crime_rate;

                    listData.Add(data);
                    data = new rating_it3();

                }
                return listData;
            }

            #endregion


            #region conditin3
            //Debug.WriteLine("Display:");
            //Debug.WriteLine(suburb_search.Length);
            for (int i = 0; i < suburbSearch.Length; i++)
            {
                switch (suburbSearch[i])
                {
                    case 1:
                        Debug.WriteLine("Near CBD");
                        veryNearDistanceToCity = "Very Near";
                        break;
                    case 2:
                        Debug.WriteLine("More shops");
                        veryHighMoreshops = "Very High";
                        break;
                    case 3:
                        Debug.WriteLine("More health service");
                        veryHighHealthServices = "Very High";
                        break;
                    case 4:
                        Debug.WriteLine("Low Accident Rate");
                        lowAccidentRate = "Low";
                        break;
                    case 5:
                        Debug.WriteLine("More aged care centre");
                        veryHighMoreAgedcare = "Very High";
                        break;
                    case 6:
                        Debug.WriteLine("Near hospital");
                        veryLessTimeToHospital = "Very Less";
                        break;
                    case 7:
                        Debug.WriteLine("Low Crime Rate");
                        lowCrimeRate = "Low";
                        break;
                    default:
                        Debug.WriteLine("Null");
                        break;
                }
            }
            if (!nationality.Equals("Preferred Nationality"))
            {
                topCountryOfBirth = nationality;
            }
            else
            {
                topCountryOfBirth = null;
            }
            ViewBag.veryNearDistanceToCity = veryNearDistanceToCity;
            ViewBag.veryHighMoreshops = veryHighMoreshops;
            ViewBag.veryHighHealthServices = veryHighHealthServices;
            ViewBag.lowAccidentRate = lowAccidentRate;
            ViewBag.veryHighMoreAgedcare = veryHighMoreAgedcare;
            ViewBag.veryLessTimeToHospital = veryLessTimeToHospital;
            ViewBag.lowCrimeRate = lowCrimeRate;
            ViewBag.topCountryOfBirth = topCountryOfBirth;
            ViewBag.resultCount = db1.SearchByPreference(veryNearDistanceToCity, veryHighMoreshops, veryHighHealthServices, lowAccidentRate, veryHighMoreAgedcare, veryLessTimeToHospital, lowCrimeRate, topCountryOfBirth, null).Count();


            //Session["nationality"] = nationality;
            //Session["suburbSearch"] = suburbSearch;

            /////
            IEnumerable<rating_it3> reslt = db1.SearchByPreference(veryNearDistanceToCity, veryHighMoreshops, veryHighHealthServices, lowAccidentRate, veryHighMoreAgedcare, veryLessTimeToHospital, lowCrimeRate, topCountryOfBirth, null);

            foreach (var item in reslt)
            {
                data.suburb_id = item.suburb_id;
                data.suburb = item.suburb;
                data.distance_to_city_rate = item.distance_to_city_rate;
                data.population_density = item.population_density;

                data.distance_to_city = item.distance_to_city;
                data.commercial_percent = item.commercial_percent;
                data.commercial_rate = item.commercial_rate;
                data.health_services = item.health_services;
                data.accident_count = item.accident_count;
                data.accident_count_rate = item.accident_count_rate;
                data.aged_care = item.aged_care;
                data.time_to_hospital = item.time_to_hospital;
                data.offence_count = item.offence_count;
                data.crime_rate = item.crime_rate;

                listData.Add(data);
                data = new rating_it3();

            }
            return listData;
            /////
            #endregion
        }

        public List<rating_it3> Search_SearchBySuburb_(string inputSuburb)
        {
            Session["inputSuburb"] = inputSuburb;
            List<rating_it3> listData = new List<rating_it3>();
            rating_it3 data = new rating_it3();
            SuburbDataEntities db1 = new SuburbDataEntities();

            ViewBag.inputSuburb = inputSuburb;
            ViewBag.noResult = "false";
            if (inputSuburb.Equals(""))
            {
                ViewBag.noResult = "true";
                return null;
            }
            //var userInput = inputSuburb;
            ////Debug.WriteLine(userInput);
            //var suburbs = from s in db.suburb_info where  s.community_name.Contains(inputSuburb) select s;
            var suburbs = from s in db1.rating_it3 where s.suburb.Contains(inputSuburb) select s;
            if (suburbs.Count() == 0)
            {
                suburbs = from s in db1.rating_it3 where s.postcode.ToString().Equals(inputSuburb) select s;
            }
            if (suburbs.Count() == 0)
            {
                ViewBag.noResult = "true";
            }


            return suburbs.ToList();
            //return View();
        }


        public List<rating_it3> Search_SearchBySuburbChange_(string suburbChange)
        {
            List<rating_it3> listData = new List<rating_it3>();
            rating_it3 data = new rating_it3();
            Session["suburbChange"] = suburbChange;
            SuburbDataEntities db1 = new SuburbDataEntities();

            if (suburbChange.Equals("I want to live near by the sea"))
            {
                var res1 = db1.SearchBySuburbChange("sea change");

                foreach (var item in res1)
                {
                    data.suburb_id = item.suburb_id;
                    data.suburb = item.suburb;
                    data.crime_rate = item.crime_rate;
                    data.accident_count_rate = item.accident_count_rate;
                    data.health_services = item.health_services;
                    data.distance_to_city_rate = item.distance_to_city_rate;

                    data.Public_Transport_Freq = item.Public_Transport_Freq;
                    data.golf_count = item.golf_count;
                    data.distance_to_fishspot = item.distance_to_fishspot;
                    listData.Add(data);
                    data = new rating_it3();

                }

                return listData;
            }
            else if (suburbChange.Equals("I want to live near by the green"))
            {
                var res2 = db1.SearchBySuburbChange("tree change");
                foreach (var item in res2)
                {
                    data.suburb_id = item.suburb_id;
                    data.suburb = item.suburb;
                    data.crime_rate = item.crime_rate;
                    data.accident_count_rate = item.accident_count_rate;
                    data.health_services = item.health_services;
                    data.distance_to_city_rate = item.distance_to_city_rate;

                    data.distance_nearest_public_hospital = item.distance_nearest_public_hospital;
                    data.Public_Transport_Freq = item.Public_Transport_Freq;
                    data.nursery_shops = item.nursery_shops;
                    listData.Add(data);
                    data = new rating_it3();

                }
                return listData;
            }
            else if (suburbChange.Equals("I want to live near by the city"))
            {
                var res3 = db1.SearchBySuburbChange("city change");
                foreach (var item in res3)
                {
                    data.suburb_id = item.suburb_id;
                    data.suburb = item.suburb;
                    data.crime_rate = item.crime_rate;
                    data.accident_count_rate = item.accident_count_rate;
                    data.health_services = item.health_services;
                    data.distance_to_city_rate = item.distance_to_city_rate;

                    data.buy_unit_2br_string = item.buy_unit_2br_string;
                    data.cafes_count = item.cafes_count;
                    data.dining_count = item.dining_count;
                    data.bar_count = item.bar_count;
                    listData.Add(data);
                    data = new rating_it3();

                }
                return listData;
            }
            else
            {
                return null;
            }
        }

        #endregion

        public ActionResult Compare(int[] suburbb)
        {
            if (suburbb == null || (suburbb.Count() > 2 || suburbb.Count() < 1))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SuburbAndSuburbList model = new SuburbAndSuburbList();

            object suburbSearch = null;
            string nationality = "";
            string suburbChange = "";
            string inputSuburb = "";

            if (Session["searchMethod"] == "Index")
            {
                ViewBag.SearchResultPage = "SearchByPreference";
                suburbSearch = Session["suburbSearch"] == null ? null : Session["suburbSearch"];
                nationality = Session["nationality"] == null ? null : Session["nationality"].ToString();
                model.SuburbListData = Search_Index_((suburbSearch as int[]), nationality);
                ViewBag.suburbList = model.SuburbListData;
            }
            else if (Session["searchMethod"] == "SearchBySuburb")
            {
                inputSuburb = Session["inputSuburb"] == null ? null : Session["inputSuburb"].ToString();
                model.SuburbListData = Search_SearchBySuburb_(inputSuburb);
            }
            else if (Session["searchMethod"] == "SearchBySuburbChange")
            {
                ViewBag.SearchResultPage = "SearchByLifeStyle";
                suburbChange = Session["suburbChange"] == null ? null : Session["suburbChange"].ToString();
                if (suburbChange.Equals("I want to live near by the sea"))
                {
                    ViewBag.LifeStyle = "SeaChange";
                }else if (suburbChange.Equals("I want to live near by the green"))
                {
                    ViewBag.LifeStyle = "TreeChange";
                }else if (suburbChange.Equals("I want to live near by the city"))
                {
                    ViewBag.LifeStyle = "CityChange";
                }
                model.SuburbListData = Search_SearchBySuburbChange_(suburbChange);
            }

            List<rating_it3> listRatingit2Compare = suburbData.rating_it3.Where(w => suburbb.Contains(w.suburb_id)).ToList();
            model.SuburbListDataCompare = listRatingit2Compare;

            return View(model);
        }












        // GET: /Suburb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            suburb_info suburb_info = db.suburb_info.Find(id);
            if (suburb_info == null)
            {
                return HttpNotFound();
            }
            return View(suburb_info);
        }

        // POST: /Suburb/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="community_name,region,map_reference,grid_reference,location,population_density,travel_time_gpo,distance_to_gpo,lga,primary_care_partnership,medicare_local,area,aria_min,aria_max,aria_avg,abs_remoteness_category,dhs_area,commercial,commercial_percent,industrial,industrial_percent,residential,residential_percent,rural,rural_percent,other,other_percent,erp_age_0_4_2012,erp_age_0_4_percent_2012,erp_age_5_9_2012,erp_age_5_9_percent_2012,erp_age_10_14_2012,erp_age_10_14_percent_2012,erp_age_15_19_2012,erp_age_15_19_percent_2012,erp_age_20_24_2012,erp_age_20_24_percent_2012,erp_age_25_44_2012,erp_age_25_44_percent_2012,erp_age_45_64_2012,erp_age_45_64_percent_2012,erp_age_65_69_2012,erp_age_65_69_percent_2012,erp_age_70_74_2012,erp_age_70_74_percent_2012,erp_age_75_79_2012,erp_age_75_79_percent_2012,erp_age_80_84_2012,erp_age_80_84_percent_2012,erp_age_85_plus_2012,erp_age_85_plus_percent_2012,erp_total_2012,erp_age_0_4_2007,erp_age_0_4_percent_2007,erp_age_5_9_2007,erp_age_5_9_percent_2007,erp_age_10_14_2007,erp_age_10_14_percent_2007,erp_age_15_19_2007,erp_age_15_19_percent_2007,erp_age_20_24_2007,erp_age_20_24_percent_2007,erp_age_25_44_2007,erp_age_25_44_percent_2007,erp_age_45_64_2007,erp_age_45_64_percent_2007,erp_age_65_69_2007,erp_age_65_69_percent_2007,erp_age_70_74_2007,erp_age_70_74_percent_2007,erp_age_75_79_2007,erp_age_75_79_percent_2007,erp_age_80_84_2007,erp_age_80_84_percent_2007,erp_age_85_plus_2007,erp_age_85_plus_percent_2007,erp_total_2007,change_2007_2012_age_0_4,change_2007_2012_age_5_9,change_2007_2012_age_10_14,change_2007_2012_age_15_19,change_2007_2012_age_20_24,change_2007_2012_age_25_44,change_2007_2012_age_45_64,change_2007_2012_age_65_69,change_2007_2012_age_70_74,change_2007_2012_age_75_79,change_2007_2012_age_80_84,change_2007_2012_age_85_plus,change_2007_2012_total,public_hospitals,private_hospitals,community_health_centres,bush_nursing_centres,allied_health,alternative_health,child_protection_and_family,dental,disability,general_practice,homelessness,mental_health,pharmacies,aged_care_high_care,aged_care_low_care,aged_care_srs,childcare,primary_schools,secondary_schools,p12_schools,other_schools,centrelink_offices,medicare_offices,medicare_access_points,number_of_households,average_persons_per_household,occupied_private_dwellings,occupied_private_dwellings_percent,population_non_private_dwellings,public_housing_dwellings,public_housing_dwellings_percent,dwellings_no_motor_vehicle,dwellings_no_motor_vehicle_percent,dwellings_with_no_internet,dwellings_with_no_internet_percent,equivalent_household_income_lessthan_600,equivalent_household_income_lessthan_600_percent,personal_income_lessthan_400,personal_income_lessthan_400_percent,number_of_families,female_headed_lone_parent_families,female_headed_lone_parent_families_percent,male_headed_lone_parent_families,male_headed_lone_parent_families_percent,residing_near_pt_percent,irsd_min,irsd_max,irsd_avg,primary_school_students,secondary_school_students,tafe_students,university_students,holds_degree_persons,holds_degree_persons_percent,did_not_complete_year_12,did_not_complete_year_12_percent,unemployed_persons,unemployed_persons_percent,volunteers_persons,volunteers_persons_percent,requires_assistance_core_activities_persons,requires_assistance_core_activities_persons_percent,aged_75_lives_alone_persons,aged_75_lives_alone_persons_percent,unpaid_carer_to_disability,unpaid_carer_to_disability_percent,unpaid_carer_children,unpaid_carer_children_percent,top_industry_1,top_industry_percent_1,top_industry_persons_2,top_industry_percent_2,top_industry_persons_3,top_industry_percent_3,top_occupation_1,top_occupation_percent_1,top_occupation_2,top_occupation_percent_2,top_occupation_3,top_occupation_percent_3,aboriginal_torres_strait_islander,aboriginal_torres_strait_islander_percent,born_overseas,born_overseas_percent,born_non_english_speaking_country,born_non_english_speaking_country_percent,speaks_lote_home,speaks_lote_home_percent,poor_english_proficiency,poor_english_proficiency_percent,top_country_of_birth_1,top_country_of_birth_persons_1,top_country_of_birth_percent_1,top_country_of_birth_2,top_country_of_birth_persons_2,top_country_of_birth_percent_2,top_country_of_birth_3,top_country_of_birth_person_3,top_country_of_birth_percent_3,top_country_of_birth_4,top_country_of_birth_person_4,top_country_of_birth_percent_4,top_country_of_birth_5,top_country_of_birth_person_5,top_country_of_birth_percent_5,top_language_spoken_1,top_language_spoken_person_1,top_language_spoken_percent_1,top_language_spoken_2,top_language_spoken_persons_2,top_language_spoken_percent_2,top_language_spoken_3,top_language_spoken_persons_3,top_language_spoken_percent_3,top_language_spoken_4,top_language_spoken_persons_4,top_language_spoken_pecent_4,top_language_spoken_5,top_language_spoken_persons_5,top_language_spoken_percent_5,public_hospital_separations,nearest_public_hospital,travel_time_nearest_public_hospital,distance_nearest_public_hospital,obstetric_type_separations,public_hospital_maternity_services,public_hospital_maternity_services_time,public_hospital_maternity_services_distance,presentations_emergency_departments,public_hospital_emergency_department,public_hospital_emergency_department_time,public_hospital_emergency_department_distance,presentations_emergency_departments_injury,presentations_emergency_departments_injury_percent,emergency_department_presentations,emergency_department_presentations_percent,suburb_id,postcode,suburb,state,dc,type,lat,lon")] suburb_info suburb_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suburb_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suburb_info);
        }

        // GET: /Suburb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            suburb_info suburb_info = db.suburb_info.Find(id);
            if (suburb_info == null)
            {
                return HttpNotFound();
            }
            return View(suburb_info);
        }

        // POST: /Suburb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            suburb_info suburb_info = db.suburb_info.Find(id);
            db.suburb_info.Remove(suburb_info);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
